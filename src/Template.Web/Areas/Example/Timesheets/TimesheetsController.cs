using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Template.Services.Shared;
using Template.Web.Infrastructure;
using Template.Web.SignalR;
using Template.Web.SignalR.Hubs.Events;
using System.Linq;

namespace Template.Web.Areas.Example.Timesheets
{
    [Area("Example")]
    public partial class TimesheetsController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;
        private readonly IPublishDomainEvents _publisher;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TimesheetsController(SharedService sharedService, IPublishDomainEvents publisher, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedService = sharedService;
            _publisher = publisher;
            _sharedLocalizer = sharedLocalizer;

            ModelUnbinderHelpers.ModelUnbinders.Add(typeof(IndexViewModel), new SimplePropertyModelUnbinder());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index(IndexViewModel model)
        {
            var timesheets = await _sharedService.Query(model.ToTimesheetsIndexQuery());
            model.SetTimesheets(timesheets);

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
                model.SetTimesheet(await _sharedService.Query(new TimesheetDetailQuery
                {
                    Id = id.Value,
                }));
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Edit(EditViewModel model, string WeekDaysEncoded)
        {
            Console.WriteLine($"POST Edit ricevuto - Name: {model.Name}, WeekDay: {model.WeekDay}, StartTime: {model.StartTime}, EndTime: {model.EndTime}");

            if (!string.IsNullOrEmpty(WeekDaysEncoded))
            {
                model.WeekDay = WeekDaysEncoded;

            }

            if (string.IsNullOrEmpty(model.WeekDay))
            {
                ModelState.AddModelError(nameof(model.WeekDay), "Seleziona almeno un giorno.");
            }

            if (!ModelState.IsValid)
            {
                Alerts.AddError(this, "Errore in aggiornamento");
                return RedirectToAction(nameof(Index));
            }

           try
            {
                model.Id = await _sharedService.HandleTimesheet(model.ToAddOrUpdateTimesheetCommand());
                Alerts.AddSuccess(this, "Informazioni aggiornate");
            }
            catch (Exception e)
            {
                Console.WriteLine("Eccezione HandleTimesheet: " + e.Message);
                ModelState.AddModelError(string.Empty, e.Message);
                Alerts.AddError(this, "Errore in aggiornamento");
            }
            Console.WriteLine("Uscendo");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            Console.WriteLine("ENTRATO IN DELETE - ID: " + id);

            try
            {
                var usersWithTimesheet = await _sharedService.Query(new UsersByTimesheetQuery
                {
                    TimesheetId = id
                });

                if (usersWithTimesheet.Users.Any())
                {
                    Alerts.AddError(this, "Non puoi cancellare questo timesheet perché ci sono utenti associati.");
                    return RedirectToAction(nameof(Index));
                }
                Console.WriteLine("ENTRATO IN DELETE - ID: " + id);
                await _sharedService.DeleteTimesheet(id);
                Alerts.AddSuccess(this, "Timesheet cancellato");
            }
            catch (Exception e)
            {
                Alerts.AddError(this, "Errore nella cancellazione: " + e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
