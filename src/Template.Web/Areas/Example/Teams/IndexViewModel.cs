using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Web.Areas.Example.Teams
{
    public class IndexViewModel : PagingViewModel
    {
        public IndexViewModel()
        {
            OrderBy = nameof(TeamIndexViewModel.Name);
            OrderByDescending = false;
            Teams = Array.Empty<TeamIndexViewModel>();
        }

        [Display(Name = "Cerca")]
        public string Filter { get; set; }

        public IEnumerable<TeamIndexViewModel> Teams { get; set; }

        internal void SetTeams(TeamsIndexDTO teamsIndexDTO)
        {
            Teams = teamsIndexDTO.Teams.Select(x => new TeamIndexViewModel(x)).ToArray();
            TotalItems = teamsIndexDTO.Count;
        }

        public TeamsIndexQuery ToTeamsIndexQuery()
        {
            return new TeamsIndexQuery
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

        public override IActionResult GetRoute() => MVC.Example.Teams.Index(this).GetAwaiter().GetResult();

        public string OrderbyUrl<TProperty>(IUrlHelper url, System.Linq.Expressions.Expression<Func<TeamIndexViewModel, TProperty>> expression) => base.OrderbyUrl(url, expression);

        public string OrderbyCss<TProperty>(HttpContext context, System.Linq.Expressions.Expression<Func<TeamIndexViewModel, TProperty>> expression) => base.OrderbyCss(context, expression);

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }

    public class TeamIndexViewModel
    {
        public TeamIndexViewModel(TeamsIndexDTO.Team teamIndexDTO)
        {
            this.Id = teamIndexDTO.Id;
            this.Name = teamIndexDTO.Name;
        }


        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
