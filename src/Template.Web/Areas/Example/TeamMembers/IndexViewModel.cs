using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Template.Web.Areas.Example.TeamMembers
{
    public class TeamWithUserCountViewModel
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public int UserCount { get; set; }
        public string ManagerName { get; set; }
    }

    public class IndexViewModel : PagingViewModel
    {
        public List<TeamWithUserCountViewModel> TeamsWithUserCount { get; set; } = new();
        public List<UserDetailDTO> Users { get; set; } = new List<UserDetailDTO>();
        public List<SelectListItem> Teams { get; set; } = new List<SelectListItem>();
        public List<TeamMemberWithUserDetailViewModel> EnrichedTeamMembers { get; set; } = new List<TeamMemberWithUserDetailViewModel>();

        public string Filter { get; set; }
        public IEnumerable<TeamMemberIndexViewModel> TeamMembers { get; set; } = Array.Empty<TeamMemberIndexViewModel>();

        public IndexViewModel()
        {
            OrderBy = nameof(TeamMemberIndexViewModel.TeamId);
            OrderByDescending = false;
        }

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
            Id = teamMemberIndexDTO.Id;
            TeamId = teamMemberIndexDTO.TeamId;
            UserId = teamMemberIndexDTO.UserId;
            IsManager = teamMemberIndexDTO.IsManager;
        }

        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid UserId { get; set; }
        public bool IsManager { get; set; }
    }

    public class TeamMemberWithUserDetailViewModel
    {
        public Guid TeamMemberId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string TeamName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Guid TeamId { get; set; }
        public bool IsManager { get; set; }
    }
}