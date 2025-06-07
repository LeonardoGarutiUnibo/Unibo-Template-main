using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Template.Services.Shared;

namespace Template.Web.Areas.Example.Requests
{
    [Area("Example")]
    public partial class RequestsController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;

        public RequestsController(SharedService sharedService)
        {
            _sharedService = sharedService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var model = new UserIndexViewModel();

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var currentUser = await _sharedService.Query(new UserDetailQuery { Id = userId });

            var managerInfo = await _sharedService.QueryTeamMemberByUserIdAndRole(currentUser.Id, true);
            if (managerInfo == null)
                return View(model);

            var usersInfo = await _sharedService.QueryTeamMemberUsers(managerInfo.TeamId, false);

            if (usersInfo == null || !usersInfo.Any())
            {
                Console.WriteLine("Ancora nessun membro nel Team");
                return View(model);
            }

            Console.WriteLine("Membri del team trovati " + usersInfo.Count);

            var userIds = usersInfo.Select(x => x.UserId).ToList();

            var userFullNames = await _sharedService.QueryUserFullNamesByIds(userIds);

            var absencesResult = await _sharedService.Query(new AbsenceEventsIndexQuery
            {
                UserId = userIds
            });

            Console.WriteLine(userIds + " " + absencesResult.Count);

            model.Absences = absencesResult.AbsenceEvents.Select(a => new AbsenceEventViewModel {
                Id = a.Id,
                UserId = a.UserId,
                RequestDate = a.RequestDate,
                StartEventDate = a.StartEventDate,
                EndEventDate = a.EndEventDate,
                EventType = a.EventType,
                EventState = a.EventState,
                FullName = userFullNames.TryGetValue(a.UserId, out var name) ? name : "Utente sconosciuto"
            }).ToList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> UpdateAbsenceState(Guid id, string newState)
        {
            await _sharedService.UpdateAbsenceEventState(id, newState);
            return RedirectToAction("Index");
        }
    }
}
