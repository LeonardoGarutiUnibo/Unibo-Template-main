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

namespace Template.Web.Areas.Example.MyRequests
{
    [Area("Example")]
    public partial class MyRequestsController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;

        public MyRequestsController(SharedService sharedService)
        {
            _sharedService = sharedService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index()
        {
            var model = new UserIndexViewModel();
            return View(model);
        }
    }
}
