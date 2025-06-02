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

        public class UpdateUserTeamCommand
        {
            public Guid UserId { get; set; }
            public Guid? TeamId { get; set; } // Nullable se vuoi anche permettere la rimozione
        }

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
            var teamMembersDto = await _sharedService.Query(new TeamMembersIndexQuery
            {
                TeamId = teamId,
                IdCurrentTeamMember = Guid.Empty,
                Paging = null
            });

            var usersDto = await _sharedService.Query(new UsersIndexQuery
            {
                IdCurrentUser = Guid.Empty,
                Paging = null
            });

            var userDictionary = usersDto.Users.ToDictionary(u => u.Id, u => u);

            var teamUsers = teamMembersDto.TeamMembers
                .Where(tm => tm.TeamId == teamId)
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
                TeamId = u.TeamId,
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
        public virtual async Task<IActionResult> GetAvailableUsersWithoutTeamManager()
        {
            var teamMembers = await _sharedService.Query(new TeamMembersIndexQuery
            {
                IdCurrentTeamMember = Guid.Empty,
                Paging = null
            });

            var allUsers = await _sharedService.Query(new UsersIndexQuery
            {
                IdCurrentUser = Guid.Empty,
                Paging = null
            });

            var managerIds = teamMembers.TeamMembers.Where(tm => tm.IsManager).Select(tm => tm.UserId).ToHashSet();

            var availableUsers = allUsers.Users
                .Where(u => !managerIds.Contains(u.Id))
                .Select(u => new
                {
                    id = u.Id,
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    teamId = u.TeamId,
                    email = u.Email
                })
                .ToList();

            return Json(availableUsers);
        }

        [HttpGet]
        public virtual IActionResult New()
        {
            return RedirectToAction(Actions.Edit());
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAvailableUsersForManager(Guid teamId)
        {
            var usersDto = await _sharedService.Query(new UsersIndexQuery());

            var teamMembersDto = await _sharedService.Query(new TeamMembersIndexQuery
            {
                TeamId = Guid.Empty,
                IdCurrentTeamMember = Guid.Empty,
                Paging = null
            });

            var userIdsAlreadyInTeam = teamMembersDto.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .Select(tm => tm.UserId)
                .ToHashSet();

            var availableUsers = usersDto.Users
                .Where(u => !userIdsAlreadyInTeam.Contains(u.Id))
                .Select(u => new
                {
                    id = u.Id,
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    email = u.Email
                });

            return Json(availableUsers);
        }

        [HttpPost]
        public virtual async Task<IActionResult> AssignTeam([FromBody] AssignTeamRequest request)
        {
            if (!Guid.TryParse(request.UserId, out var userId) || !Guid.TryParse(request.TeamId, out var teamId))
                return BadRequest("Parametri non validi.");

            var allAssignments = await _sharedService.Query(new TeamMembersIndexQuery
            {
                TeamId = Guid.Empty,
                IdCurrentTeamMember = Guid.Empty,
                Paging = null
            });

            var userAssignments = allAssignments.TeamMembers.Where(tm => tm.UserId == userId).ToList();
            Console.WriteLine($"UserId: {request.UserId}, TeamId: {request.TeamId}, IsManager: {request.IsManager}");
            bool isAlreadyAssignedToAnotherTeam = userAssignments
            .Any(tm => tm.TeamId != teamId && tm.IsManager == request.IsManager);
            if (isAlreadyAssignedToAnotherTeam)
            {
                var oldAssignment = userAssignments.FirstOrDefault(tm => tm.IsManager == request.IsManager);

                if (oldAssignment != null)
                {
                    await _sharedService.DeleteTeamMember(oldAssignment.Id);
                }
            }
            if (!request.IsManager)
            {
                var isManagerInTeam = userAssignments.Any(tm => tm.TeamId == teamId && tm.IsManager);
                if (isManagerInTeam)
                {
                    return BadRequest("L'utente è già manager di questo team e non può essere assegnato come membro.");
                }
            }

            if (request.IsManager)
            {   
                var isMemberInTeam = userAssignments.Any(tm => tm.TeamId == teamId && !tm.IsManager);
                if (isMemberInTeam)
                {
                    return BadRequest("L'utente è già membro di questo team e non può essere assegnato come manager.");
                }
            }
            var existingMember = userAssignments.FirstOrDefault(tm => tm.TeamId == teamId);

            var command = new AddOrUpdateTeamMemberCommand
            {
                Id = existingMember?.Id,
                UserId = userId,
                TeamId = teamId,
                IsManager = request.IsManager
            };
            await _sharedService.HandleTeamMember(command);
            return Ok("Team aggiornato.");
        }

        public class AssignTeamRequest
        {
            public string UserId { get; set; }
            public string TeamId { get; set; }
            public bool IsManager { get; set; } = false;
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
                TeamId = u.TeamId,
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
