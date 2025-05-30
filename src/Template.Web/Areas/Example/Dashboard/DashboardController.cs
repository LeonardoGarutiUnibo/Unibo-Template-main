using Template.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Template.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public virtual async Task<IActionResult> Index()
        {
            var users = await _sharedService.Query(new UsersIndexQuery());

            var model = new UserIndexViewModel
            {
                MessaggioBenvenuto = "Benvenuto nella tua dashboard!",
                Users = users.Users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).ToList()
            };

            return View(model);
        }
        
    }
}
