﻿using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Template.Web.Areas.Example.Users
{
    public class IndexViewModel : PagingViewModel
    {
        public List<SelectListItem> Timesheets { get; set; } = new List<SelectListItem>();
        public IndexViewModel()
        {
            OrderBy = nameof(UserIndexViewModel.Email);
            OrderByDescending = false;
            Users = Array.Empty<UserIndexViewModel>();
        }

        [Display(Name = "Cerca")]
        public string Filter { get; set; }

        public IEnumerable<UserIndexViewModel> Users { get; set; }

        internal void SetUsers(UsersIndexDTO usersIndexDTO)
        {
            Users = usersIndexDTO.Users.Select(x => new UserIndexViewModel(x)).ToArray();
            TotalItems = usersIndexDTO.Count;
        }

        public UsersIndexQuery ToUsersIndexQuery()
        {
            return new UsersIndexQuery
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

        public override IActionResult GetRoute() => MVC.Example.Users.Index(this).GetAwaiter().GetResult();

        public string OrderbyUrl<TProperty>(IUrlHelper url, System.Linq.Expressions.Expression<Func<UserIndexViewModel, TProperty>> expression) => base.OrderbyUrl(url, expression);

        public string OrderbyCss<TProperty>(HttpContext context, System.Linq.Expressions.Expression<Func<UserIndexViewModel, TProperty>> expression) => base.OrderbyCss(context, expression);

        public string ToJson()
        {
            return JsonSerializer.ToJsonCamelCase(this);
        }
    }

    public class UserIndexViewModel
    {
        public UserIndexViewModel(UsersIndexDTO.User userIndexDTO)
        {
            this.Id = userIndexDTO.Id;
            this.Email = userIndexDTO.Email;
            this.FirstName = userIndexDTO.FirstName;
            this.LastName = userIndexDTO.LastName;
            this.NickName = userIndexDTO.NickName;
            this.Role = userIndexDTO.Role;
            this.TimesheetId = userIndexDTO.TimesheetId;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Role { get; set; }
        public Guid TimesheetId { get; set; }
        public List<SelectListItem> Timesheets { get; set; } = new();
    }
}
