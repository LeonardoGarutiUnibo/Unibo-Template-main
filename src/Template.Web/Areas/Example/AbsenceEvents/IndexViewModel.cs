using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Web.Areas.Example.AbsenceEvents
{
    public class IndexViewModel : PagingViewModel
    {
        public IndexViewModel()
        {
            OrderBy = nameof(AbsenceEventIndexViewModel.UserId);
            OrderByDescending = false;
            AbsenceEvents = Array.Empty<AbsenceEventIndexViewModel>();
        }

        public string Filter { get; set; }

        public IEnumerable<AbsenceEventIndexViewModel> AbsenceEvents { get; set; }

        internal void SetAbsenceEvents(AbsenceEventsIndexDTO absenceEventsIndexDTO)
        {
            AbsenceEvents = absenceEventsIndexDTO.AbsenceEvents.Select(x => new AbsenceEventIndexViewModel(x)).ToArray();
            TotalItems = absenceEventsIndexDTO.Count;
        }

        public AbsenceEventsIndexQuery ToAbsenceEventsIndexQuery()
        {
            return new AbsenceEventsIndexQuery
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

        public override IActionResult GetRoute() => MVC.Example.AbsenceEvents.Index(this).GetAwaiter().GetResult();

        public string OrderbyUrl<TProperty>(IUrlHelper url, System.Linq.Expressions.Expression<Func<AbsenceEventIndexViewModel, TProperty>> expression) => base.OrderbyUrl(url, expression);

        public string OrderbyCss<TProperty>(HttpContext context, System.Linq.Expressions.Expression<Func<AbsenceEventIndexViewModel, TProperty>> expression) => base.OrderbyCss(context, expression);

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }

    public class AbsenceEventIndexViewModel
    {
        public AbsenceEventIndexViewModel(AbsenceEventsIndexDTO.AbsenceEvent absenceEventIndexDTO)
        {
            this.Id = absenceEventIndexDTO.Id;
            this.UserId = absenceEventIndexDTO.UserId;
            this.RequestDate = absenceEventIndexDTO.RequestDate;
            this.StartEventDate = absenceEventIndexDTO.StartEventDate;
            this.EndEventDate = absenceEventIndexDTO.EndEventDate;
            this.EventType = absenceEventIndexDTO.EventType;
            this.EventState = absenceEventIndexDTO.EventState;
        }


        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }
        public string EventType { get; set; }
        public string EventState { get; set; }
    }
}
