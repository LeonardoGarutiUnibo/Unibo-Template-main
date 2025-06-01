using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template.Services.Shared;
using Template.Web.Infrastructure;
using Template.Web.SignalR;
using Template.Web.SignalR.Hubs.Events;

namespace Template.Web.Areas.Example.TeamMembers
{
    [Area("Example")]
    public partial class TeamMembersController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;
        private readonly IPublishDomainEvents _publisher;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamMembersController(
            SharedService sharedService,
            IPublishDomainEvents publisher,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedService = sharedService;
            _publisher = publisher;
            _sharedLocalizer = sharedLocalizer;

            ModelUnbinderHelpers.ModelUnbinders.Add(typeof(IndexViewModel), new SimplePropertyModelUnbinder());
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetUsers(Guid teamId)
        {
            // Ottieni i membri del team specificato
            var teamMembersDto = await _sharedService.Query(new TeamMembersIndexQuery
            {
                TeamId = teamId,
                IdCurrentTeamMember = Guid.Empty,
                Paging = null
            });

            // Ottieni tutti gli utenti
            var usersDto = await _sharedService.Query(new UsersIndexQuery
            {
                IdCurrentUser = Guid.Empty,
                Paging = null
            });

            // Crea dizionario per lookup veloce degli utenti
            var userDictionary = usersDto.Users.ToDictionary(u => u.Id, u => u);

            // ✅ Filtro: solo membri del team specificato
            var teamUsers = teamMembersDto.TeamMembers
                .Where(tm => tm.TeamId == teamId) // <--- questo filtro è fondamentale
                .Where(tm => userDictionary.ContainsKey(tm.UserId))
                .Select(tm =>
                {
                    var user = userDictionary[tm.UserId];
                    return new
                    {
                        userId = user.Id,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        email = user.Email,
                        role = user.Role,
                        isManager = tm.IsManager,
                        teamId = tm.TeamId
                    };
                })
                .ToList();

            return Json(teamUsers);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index(IndexViewModel model)
        {
        
            var teamMembersDto = await _sharedService.Query(model.ToTeamMembersIndexQuery());
            model.SetTeamMembers(teamMembersDto);

            var usersDto = await _sharedService.Query(new UsersIndexQuery
            {
                IdCurrentUser = Guid.Empty,
                Paging = null
            });

            model.Users = usersDto.Users.Select(u => new UserDetailDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                NickName = u.NickName,
                Email = u.Email,
                Role = u.Role
            }).ToList();

            var teamsDto = await _sharedService.Query(new TeamsIndexQuery
            {
                IdCurrentTeam = Guid.Empty,
                Paging = null
            });

            model.Teams = teamsDto.Teams.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();


            model.TeamsWithUserCount = teamsDto.Teams.Select(team =>
            {
            
                var managerMember = teamMembersDto.TeamMembers
                    .FirstOrDefault(tm => tm.TeamId == team.Id && tm.IsManager);
    
                string managerName = "";
                if (managerMember != null)
                {
                    var managerUser = model.Users.FirstOrDefault(u => u.Id == managerMember.UserId);
                    if (managerUser != null)
                    {
                        managerName = $"{managerUser.FirstName} {managerUser.LastName}";
                    }
                }
    
                return new TeamWithUserCountViewModel
                {
                    TeamId = team.Id,
                    TeamName = team.Name,
                    UserCount = teamMembersDto.TeamMembers.Count(tm => tm.TeamId == team.Id),
                    ManagerName = managerName
                };
            }).ToList();
    
            return View(model);
        }

        [HttpGet]
        public virtual IActionResult New()
        {
            return RedirectToAction(Actions.Edit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> AssignTeam([FromBody] AssignTeamRequest request)
        {

            if (!Guid.TryParse(request.UserId, out var userId) || !Guid.TryParse(request.TeamId, out var teamId))
                return BadRequest("Parametri non validi.");

            var existing = await _sharedService.Query(new TeamMembersIndexQuery
            {
                TeamId = Guid.Empty,
                IdCurrentTeamMember = Guid.Empty,
                Paging = null
            });

            var existingMember = existing.TeamMembers.FirstOrDefault(tm => tm.UserId == userId);

            var command = new AddOrUpdateTeamMemberCommand
            {
                Id = existingMember?.Id,
                UserId = userId,
                TeamId = teamId,
                IsManager = existingMember?.IsManager ?? false 
            };

            Console.WriteLine("Chiamata per inserimento nuovo utente");
            await _sharedService.HandleTeamMember(command);
            Console.WriteLine("Fine chiamata per inserimento nuovo utente");

            return Ok("Team assegnato correttamente.");
        }

        public class AssignTeamRequest
        {
            public string UserId { get; set; }
            public string TeamId { get; set; }
        }

        [HttpGet]
        public virtual async Task<IActionResult> Edit(Guid? id)
        {
            var model = new EditViewModel();

            if (id.HasValue)
            {
                model.SetTeamMember(await _sharedService.Query(new TeamMemberDetailQuery
                {
                    Id = id.Value,
                }));
            }
            
            var userQuery = new UsersIndexQuery
            {
                IdCurrentUser = Guid.Empty,
                Paging = null
            };

            var usersDto = await _sharedService.Query(userQuery);

            model.Users = usersDto.Users.Select(u => new UserDetailDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                NickName = u.NickName,
                Email = u.Email,
                Role = u.Role
            }).ToList();

             var teamQuery = new TeamsIndexQuery
            {
                IdCurrentTeam = Guid.Empty,
                Paging = null
            };

            var teamsDto = await _sharedService.Query(teamQuery);

            model.Teams = teamsDto.Teams.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();


            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Id = await _sharedService.HandleTeamMember(model.ToAddOrUpdateTeamMemberCommand());

                    Alerts.AddSuccess(this, "Informazioni aggiornate");

                    // Esempio lancio di un evento SignalR
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            if (ModelState.IsValid == false)
            {
                Alerts.AddError(this, "Errore in aggiornamento");
            }

            return RedirectToAction(Actions.Edit(model.Id));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            // Query to delete user

            Alerts.AddSuccess(this, "Utente cancellato");

            return RedirectToAction(Actions.Index());
        }
    }
}
