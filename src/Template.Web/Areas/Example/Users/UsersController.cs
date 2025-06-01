using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template.Services.Shared;
using Template.Web.Infrastructure;
using Template.Web.SignalR;
using Template.Web.SignalR.Hubs.Events;

namespace Template.Web.Areas.Example.Users
{
    [Area("Example")]
    public partial class UsersController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;
        private readonly IPublishDomainEvents _publisher;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public UsersController(SharedService sharedService, IPublishDomainEvents publisher, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedService = sharedService;
            _publisher = publisher;
            _sharedLocalizer = sharedLocalizer;

            ModelUnbinderHelpers.ModelUnbinders.Add(typeof(IndexViewModel), new SimplePropertyModelUnbinder());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index(IndexViewModel model)
        {
            var users = await _sharedService.Query(model.ToUsersIndexQuery());
            model.SetUsers(users);

            var timesheetQuery = new TimesheetsIndexQuery
            {
                IdCurrentTimesheet = Guid.Empty,
                Paging = null
            };
            
            var timesheetsDto = await _sharedService.Query(timesheetQuery);
            
            model.Timesheets = timesheetsDto.Timesheets.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public virtual IActionResult New()
        {
            return RedirectToAction(Actions.Edit());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Edit(Guid? id)
        {
            var model = new EditViewModel();

            if (id.HasValue)
            {
                model.SetUser(await _sharedService.Query(new UserDetailQuery
                {
                    Id = id.Value,
                }));
            }

            var timesheetQuery = new TimesheetsIndexQuery
            {
                IdCurrentTimesheet = Guid.Empty,
                Paging = null 
            };

            var timesheetsDto = await _sharedService.Query(timesheetQuery);

            model.Timesheets = timesheetsDto.Timesheets.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(EditViewModel model)
        {
            Console.WriteLine("Role ricevuto: " + model.Role);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Id = await _sharedService.HandleUser(model.ToAddOrUpdateUserCommand());

                    Alerts.AddSuccess(this, "Informazioni aggiornate");

                    // Esempio lancio di un evento SignalR
                    await _publisher.Publish(new NewMessageEvent
                    {
                        IdGroup = model.Id.Value,
                        IdUser = model.Id.Value,
                        IdMessage = Guid.NewGuid()
                    });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                }
            }

            if (ModelState.IsValid == false)
            {
                Alerts.AddError(this, "Errore in aggiornamento");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            Console.WriteLine("ENTRATO IN DELETE - ID: " + id);

            try
            {
                await _sharedService.DeleteUser(id);
                Alerts.AddSuccess(this, "Utente cancellato");
            }
            catch (Exception e)
            {
                Alerts.AddError(this, "Errore nella cancellazione: " + e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
