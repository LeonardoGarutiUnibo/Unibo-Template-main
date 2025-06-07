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
        if (currentUser == null)
        {
            return Unauthorized();
        }
        
        if (users == null || users.Users == null)
        {
            return BadRequest("Errore nel recupero utenti");
        }
        var allUsers = users.Users
            .Where(u => u.Id != currentUser.Id)
            .Select(u => new UserScheduleViewModel
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                TimesheetWeekDay = u.TimesheetWeekDay,
                TimesheetStartTime = u.TimesheetStartTime,
                TimesheetEndTime = u.TimesheetEndTime,
                Schedule = new Dictionary<string, EventInfo>()
            }).ToList();

        var otherUsers = users.Users
            .Where(u => u.Id != currentUser.Id && u.TeamId == currentUser.TeamId)
            .Select(u => new UserScheduleViewModel
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                TimesheetWeekDay = u.TimesheetWeekDay,
                TimesheetStartTime = u.TimesheetStartTime,
                TimesheetEndTime = u.TimesheetEndTime,
                Schedule = new Dictionary<string, EventInfo>()
            }).ToList();

        var currentUserSchedule = new UserScheduleViewModel
        {
            Id = currentUser.Id,
            Name = $"{currentUser.FirstName} {currentUser.LastName}",
            TimesheetWeekDay = currentUser.TimesheetWeekDay,
            TimesheetStartTime = currentUser.TimesheetStartTime,
            TimesheetEndTime = currentUser.TimesheetEndTime,
            Schedule = new Dictionary<string, EventInfo>()
        };

        var model = new PresenzeViewModel
        {
            StartMonth = start,
            EndMonth = end,
            DaysInMonth = daysInRange,
            CurrentUserName = $"{currentUser.FirstName} {currentUser.LastName}",
            CurrentUserId = currentUser.Id,
            CurrentUserTimesheetWeekDay = currentUser.TimesheetWeekDay,
            Users = otherUsers,
            CurrentUserSchedule = currentUserSchedule
        };

        var managerInfo = await _sharedService.QueryTeamMemberByUserIdAndRole(currentUser.Id, true);
        model.IsManager = managerInfo != null;
        if (managerInfo != null){
            var usersInfo = await _sharedService.QueryTeamMemberUsers(managerInfo.TeamId, false);

            var teamUserIds = usersInfo.Select(x => x.UserId).ToHashSet();
        
            var teamUsers = users.Users
                .Where(u => teamUserIds.Contains(u.Id) && u.Id != currentUser.Id)
                .Select(u => new UserScheduleViewModel
                {
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    TimesheetWeekDay = u.TimesheetWeekDay,
                    TimesheetStartTime = u.TimesheetStartTime,
                    TimesheetEndTime = u.TimesheetEndTime,
                    Schedule = new Dictionary<string, EventInfo>()
                })
                .ToList();
        
            model.TeamUsers = teamUsers;

            var userIds = usersInfo.Select(x => x.UserId).ToList();

            var userFullNames = await _sharedService.QueryUserFullNamesByIds(userIds);
            var absencesResult = await _sharedService.Query(new AbsenceEventsIndexQuery
            {
                UserId = userIds
            });

            model.Absences = absencesResult.AbsenceEvents.Select(a => new AbsenceEventViewModel {
                UserId = a.UserId,
                StartEventDate = a.StartEventDate,
                EndEventDate = a.EndEventDate,
                EventType = a.EventType,
                EventState = a.EventState
            }).ToList();
        }
        var managerUserInfoList = await _sharedService.QueryTeamMemberUsers(currentUser.TeamId, true);
        if (managerUserInfoList != null && managerUserInfoList.Any())
        {
            var managerUser = users.Users
                .Where(u => u.Id == managerUserInfoList[0].UserId)
                .Select(u => new UserScheduleViewModel
                {
                    Id = managerUserInfoList[0].UserId,
                    Name = $"{u.FirstName} {u.LastName}",
                    TimesheetWeekDay = u.TimesheetWeekDay,
                    TimesheetStartTime = u.TimesheetStartTime,
                    TimesheetEndTime = u.TimesheetEndTime,
                    Schedule = new Dictionary<string, EventInfo>()
                })
                .ToList();
            if (managerUser.Any())
            {
                model.ManagerUser = managerUser;
            }
        }

        var query = new AbsenceEventsIndexQuery
        {
            StartEventDate = start,
            EndEventDate = end,
            EventType = null,
            EventState = null,
            UserId = users.Users.Select(u => u.Id).Append(currentUser.Id).ToList()
        };

        var result = await _sharedService.Query(query);

        var eventsVm = result.AbsenceEvents.Select(e => new AbsenceEventViewModel {
            UserId = e.UserId,
            StartEventDate = e.StartEventDate,
            EndEventDate = e.EndEventDate,
            EventState =e.EventState,
            EventType = e.EventType
        }).ToList();

        model.Events = eventsVm;
        Console.WriteLine($"Numero eventi ricevuti: {eventsVm.Count}");
        foreach (var evt in eventsVm)
        {
            Console.WriteLine($"UserId: {evt.UserId}, EventState: {evt.EventState}, EventType: {evt.EventType}, StartDate: {evt.StartEventDate}, EndDate: {evt.EndEventDate}");
            var userSchedule =
            evt.UserId == currentUser.Id
                ? currentUserSchedule
                : otherUsers.FirstOrDefault(u => u.Id == evt.UserId)
                ?? model.ManagerUser?.FirstOrDefault(u => u.Id == evt.UserId);
            if (userSchedule == null)
            {
                Console.WriteLine($"userSchedule è null per UserId {evt.UserId}");
                continue;
            }

            var currentDate = evt.StartEventDate.Date;
            var eventEndDate = evt.EndEventDate.Date;
            while (currentDate <= eventEndDate)
            {
                var key = currentDate.ToString("yyyy-MM-dd");
                userSchedule.Schedule[key] = new EventInfo
                {
                    EventType = evt.EventType,
                    EventState = evt.EventState
                };
                Console.WriteLine(evt.EventState, evt.EventType);
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
