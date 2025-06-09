using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Web.Areas.Example.Timesheets
{
    public class IndexViewModel : PagingViewModel
    {
        public List<TimesheetMemberWithUserDetailViewModel> EnrichedTimesheetMembers { get; set; } = new List<TimesheetMemberWithUserDetailViewModel>();

        public IndexViewModel()
        {
            OrderBy = nameof(TimesheetIndexViewModel.Name);
            OrderByDescending = false;
            Timesheets = Array.Empty<TimesheetIndexViewModel>();
        }

        [Display(Name = "Cerca")]
        public string Filter { get; set; }
        public IEnumerable<TimesheetIndexViewModel> Timesheets { get; set; }

        internal void SetTimesheets(TimesheetsIndexDTO timesheetsIndexDTO)
        {
            Timesheets = timesheetsIndexDTO.Timesheets.Select(x => new TimesheetIndexViewModel(x)).ToArray();
            TotalItems = timesheetsIndexDTO.Count;
        }

        public TimesheetsIndexQuery ToTimesheetsIndexQuery()
        {
            return new TimesheetsIndexQuery
            {
                Filter = Filter,
                Paging = new Template.Infrastructure.Paging
                {
                    OrderBy = OrderBy,
                    OrderByDescending = OrderByDescending,
                    Page = Page,
                    PageSize = PageSize
                }
            };
        }

        public override IActionResult GetRoute() => MVC.Example.Timesheets.Index(this).GetAwaiter().GetResult();

        public string OrderbyUrl<TProperty>(IUrlHelper url, System.Linq.Expressions.Expression<Func<TimesheetIndexViewModel, TProperty>> expression) => base.OrderbyUrl(url, expression);

        public string OrderbyCss<TProperty>(HttpContext context, System.Linq.Expressions.Expression<Func<TimesheetIndexViewModel, TProperty>> expression) => base.OrderbyCss(context, expression);

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }

    public class TimesheetMemberWithUserDetailViewModel
    {

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class TimesheetIndexViewModel
    {
        public TimesheetIndexViewModel(TimesheetsIndexDTO.Timesheet timesheetIndexDTO)
        {
            this.Id = timesheetIndexDTO.Id;
            this.Name = timesheetIndexDTO.Name;
            this.WeekDay = timesheetIndexDTO.WeekDay;
            this.StartTime = timesheetIndexDTO.StartTime;
            this.EndTime = timesheetIndexDTO.EndTime;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WeekDay { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
