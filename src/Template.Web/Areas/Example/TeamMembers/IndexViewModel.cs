using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Template.Web.Areas.Example.TeamMembers
{
    public class IndexViewModel : PagingViewModel
    {
        public IndexViewModel()
        {
            OrderBy = nameof(TeamMemberIndexViewModel.TeamId);
            OrderByDescending = false;
            TeamMembers = Array.Empty<TeamMemberIndexViewModel>();
        }

        public string Filter { get; set; }

        public IEnumerable<TeamMemberIndexViewModel> TeamMembers { get; set; }

        internal void SetTeamMembers(TeamMembersIndexDTO teamMembersIndexDTO)
        {
            TeamMembers = teamMembersIndexDTO.TeamMembers.Select(x => new TeamMemberIndexViewModel(x)).ToArray();
            TotalItems = teamMembersIndexDTO.Count;
        }

        public TeamMembersIndexQuery ToTeamMembersIndexQuery()
        {
            return new TeamMembersIndexQuery
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

        public override IActionResult GetRoute() => MVC.Example.TeamMembers.Index(this).GetAwaiter().GetResult();

        public string OrderbyUrl<TProperty>(IUrlHelper url, System.Linq.Expressions.Expression<Func<TeamMemberIndexViewModel, TProperty>> expression) => base.OrderbyUrl(url, expression);

        public string OrderbyCss<TProperty>(HttpContext context, System.Linq.Expressions.Expression<Func<TeamMemberIndexViewModel, TProperty>> expression) => base.OrderbyCss(context, expression);

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }

    public class TeamMemberIndexViewModel
    {
        public TeamMemberIndexViewModel(TeamMembersIndexDTO.TeamMember teamMemberIndexDTO)
        {
            this.Id = teamMemberIndexDTO.Id;
            this.TeamId = teamMemberIndexDTO.TeamId;
            this.UserId = teamMemberIndexDTO.UserId;
            this.IsManager = teamMemberIndexDTO.IsManager;
        }


        public Guid Id { get; set; }
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public bool IsManager { get; set; }
    }
}
