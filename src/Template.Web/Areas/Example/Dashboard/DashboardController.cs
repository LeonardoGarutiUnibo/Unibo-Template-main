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

namespace Template.Web.Areas.Example.Dashboard
{
    [Area("Example")]
    public partial class DashboardController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;

        public DashboardController(SharedService sharedService)
        {
            _sharedService = sharedService;
        }

        [HttpGet]
    public virtual async Task<IActionResult> Index(DateTime? startMonth, DateTime? endMonth)
    {
        var start = startMonth ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        var end = endMonth ?? start;

        if (start == end){
            end = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));
        }

        if (start > end)
        {
            var temp = start;
            start = end;
            end = temp;
        }

        var daysInRange = new List<string>();
        var iterator = new DateTime(start.Year, start.Month, 1);
        var endDate = new DateTime(end.Year, end.Month, DateTime.DaysInMonth(end.Year, end.Month));

        while (iterator <= endDate)
        {
            int daysInMonth = DateTime.DaysInMonth(iterator.Year, iterator.Month);
            for (int day = 1; day <= daysInMonth; day++)
            {
                daysInRange.Add($"{iterator.Year}-{iterator.Month:D2}-{day:D2}");
            }
            iterator = iterator.AddMonths(1);
        }

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        var currentUser = await _sharedService.Query(new UserDetailQuery { Id = userId });
        var users = await _sharedService.Query(new UsersIndexQuery { IdCurrentUser = userId });


        var otherUsers = users.Users
            .Where(u => u.Id != currentUser.Id)
            .Select(u => new UserScheduleViewModel
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                Schedule = new Dictionary<string, string>()
            }).ToList();

        var currentUserSchedule = new UserScheduleViewModel
        {
            Id = currentUser.Id,
            Name = $"{currentUser.FirstName} {currentUser.LastName}",
            Schedule = new Dictionary<string, string>()
        };

        var model = new PresenzeViewModel
        {
            StartMonth = start,
            EndMonth = end,
            DaysInMonth = daysInRange,
            CurrentUserName = $"{currentUser.FirstName} {currentUser.LastName}",
            CurrentUserId = currentUser.Id,
            Users = otherUsers,
            CurrentUserSchedule = currentUserSchedule
        };

        var query = new AbsenceEventsIndexQuery
        {
            StartEventDate = start,
            EndEventDate = end,
            EventType = null,
            UserId = users.Users.Select(u => u.Id).Append(currentUser.Id).ToList()
        };

        var result = await _sharedService.Query(query);

        Console.WriteLine(result.Count);

        var eventsVm = result.AbsenceEvents.Select(e => new AbsenceEventViewModel {
            UserId = e.UserId,
            StartEventDate = e.StartEventDate,
            EndEventDate = e.EndEventDate,
            EventType = e.EventType
        }).ToList();

        Console.WriteLine("eventsVm.Count");
        Console.WriteLine(eventsVm.Count);

        model.Events = eventsVm;

        foreach (var evt in eventsVm)
        {
            var userSchedule = evt.UserId == currentUser.Id ? currentUserSchedule : otherUsers.FirstOrDefault(u => u.Id == evt.UserId);
            if (userSchedule == null)
            {
                continue;
            }

            var currentDate = evt.StartEventDate.Date;
            var eventEndDate = evt.EndEventDate.Date;

            while (currentDate <= eventEndDate)
            {
                var key = currentDate.ToString("yyyy-MM-dd");
                userSchedule.Schedule[key] = evt.EventType;
                Console.WriteLine($"[DEBUG] Mapping evento: Utente {userSchedule.Name} data {key} = {evt.EventType}");
                currentDate = currentDate.AddDays(1);
            }
        }

        return View(model);
    }


        [HttpPost]
        public virtual async Task<IActionResult> SaveAbsenceEvent([FromBody] AbsenceEventInputModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dati non validi");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();
            var absenceEvent = new AbsenceEvent
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                RequestDate = DateTime.UtcNow,
                StartEventDate = model.StartEventDate,
                EndEventDate = model.EndEventDate,
                EventType = model.EventType,
                EventState = "Pending"
            };
            await _sharedService.SaveAbsenceEvent(absenceEvent);
            return Ok(new { success = true, message = "Evento salvato correttamente" });
        }
        
    }
}
