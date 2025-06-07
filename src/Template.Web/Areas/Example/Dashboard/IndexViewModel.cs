using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Web.Areas.Example.Dashboard
{
    public class UserIndexViewModel
    {
        public string MessaggioBenvenuto { get; set; }

        public List<AbsenceEventViewModel> Absences { get; set; } = new();
        public List<UserScheduleViewModel> TeamManagedUsers { get; set; } = new();
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }

    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AbsenceEventInputModel
    {
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public string EventType { get; set; }
    }

    public class AbsenceEventViewModel
    {
        public Guid UserId { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public string EventState { get; set; }
        public string EventType { get; set; }
    }

    public class PresenzeViewModel
    {
        public List<AbsenceEventViewModel> Events { get; set; } = new List<AbsenceEventViewModel>();
        public List<AbsenceEventViewModel> Absences { get; set; } = new List<AbsenceEventViewModel>();
        public string EnabledWeekDays { get; set; }
        public string CurrentUserName { get; set; }
        public UserScheduleViewModel CurrentUserSchedule { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? EndMonth { get; set; }
        public Guid CurrentUserId { get; set; }
        public bool IsManager { get; set; }
        public DateTime? SelectedMonth { get; set; }
        public List<string> DaysInMonth { get; set; } = new();
        public List<UserScheduleViewModel> Users { get; set; } = new();
        public List<UserScheduleViewModel> ManagerUser { get; set; } = new();
        public List<UserScheduleViewModel> TeamUsers { get; set; } = new();
        public string CurrentUserTimesheetWeekDay { get; set; }
        public Dictionary<string, string> StatusIcons { get; set; } = new()
        {
            ["Ferie Intera"] = "𒊹",
            ["Ferie Mezza"] = "◐",
            ["Permessi Intera"] = "■",
            ["Permessi Mezza"] = "◧",
            ["smartworking"] = "🏠︎"
        };
        public Dictionary<string, string> StatusClasses { get; set; } = new()
        {
            ["Ferie Intera"] = "Ferie Intera",
            ["Permessi Intera"] = "Permessi Intera",
            ["Ferie Mezza"] = "Ferie Intera",
            ["Permessi Mezza"] = "Permessi Mezza",
            ["smartworking"] = "smartworking"
        };
    }

    public class UserScheduleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TimesheetWeekDay { get; set; }
        public TimeSpan TimesheetStartTime { get; set; }
        public TimeSpan TimesheetEndTime { get; set; }
        public Dictionary<string, EventInfo> Schedule { get; set; } = new();
    }

    public class EventInfo
    {
        public string EventType { get; set; }
        public string EventState { get; set; }
    }
}
