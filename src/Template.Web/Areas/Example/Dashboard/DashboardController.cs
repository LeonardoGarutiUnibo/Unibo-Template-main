using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Template.Services.Shared;
using Template.Web.Infrastructure;
using Template.Web.SignalR;
using Template.Web.SignalR.Hubs.Events;

namespace Template.Web.Areas.Example.Dashboard
{
        [Area("Example")]
    public partial class DashboardController : AuthenticatedBaseController
    {
        // costruttore e altri membri...

        public virtual async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                MessaggioBenvenuto = "Benvenuto nella tua dashboard!"
            };

            return View(model);
        }
    }
}
