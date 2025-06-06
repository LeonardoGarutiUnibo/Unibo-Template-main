using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Web.Areas.Example.Requests
{
    public class UserIndexViewModel
    {
        public string MessaggioBenvenuto { get; set; }

        // Aggiungi la lista di utenti
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        public List<AbsenceEventViewModel> Absences { get; set; } = new();
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
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartEventDate { get; set; }
        public DateTime EndEventDate { get; set; }
        public string EventType { get; set; }
        public string EventState { get; set; }
        public string FullName { get; set; }
    }

    public class PresenzeViewModel
    {
        public List<AbsenceEventViewModel> Events { get; set; } = new List<AbsenceEventViewModel>();
        public string CurrentUserName { get; set; }
        public UserScheduleViewModel CurrentUserSchedule { get; set; }
        public DateTime? StartMonth { get; set; }
        public DateTime? EndMonth { get; set; }
        public Guid CurrentUserId { get; set; }
        public DateTime? SelectedMonth { get; set; }
        public List<string> DaysInMonth { get; set; } = new();
        public List<UserScheduleViewModel> Users { get; set; } = new();
        public Dictionary<string, string> StatusIcons { get; set; } = new()
        {
            ["intera"] = "●",
            ["mezza"] = "◐",
            ["assente"] = "✖",
            ["smartworking"] = "🏠︎"
        };
        public Dictionary<string, string> StatusClasses { get; set; } = new()
        {
            ["intera"] = "intera",
            ["mezza"] = "mezza",
            ["assente"] = "assente",
            ["smartworking"] = "smartworking"
        };
    }

    public class UserScheduleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> Schedule { get; set; } = new();
    }
}
