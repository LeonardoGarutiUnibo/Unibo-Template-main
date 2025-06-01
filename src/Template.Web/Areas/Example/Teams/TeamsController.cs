using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Template.Services.Shared;
using Template.Web.Infrastructure;
using Template.Web.SignalR;
using Template.Web.SignalR.Hubs.Events;

namespace Template.Web.Areas.Example.Teams
{
    [Area("Example")]
    public partial class TeamsController : AuthenticatedBaseController
    {
        private readonly SharedService _sharedService;
        private readonly IPublishDomainEvents _publisher;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public TeamsController(SharedService sharedService, IPublishDomainEvents publisher, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _sharedService = sharedService;
            _publisher = publisher;
            _sharedLocalizer = sharedLocalizer;

            ModelUnbinderHelpers.ModelUnbinders.Add(typeof(IndexViewModel), new SimplePropertyModelUnbinder());
        }

        [HttpGet]
        public virtual async Task<IActionResult> Index(IndexViewModel model)
        {
            var teams = await _sharedService.Query(model.ToTeamsIndexQuery());
            model.SetTeams(teams);

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
                model.SetTeam(await _sharedService.Query(new TeamDetailQuery
                {
                    Id = id.Value,
                }));
            }

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Console.WriteLine("Prima di chiamare HandleTeam");
                    model.Id = await _sharedService.HandleTeam(model.ToAddOrUpdateTeamCommand());
                    Console.WriteLine("Dopo chiamata HandleTeam");
                    Alerts.AddSuccess(this, "Informazioni aggiornate");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Eccezione HandleTeam: " + e.Message);
                    ModelState.AddModelError(string.Empty, e.Message);
                    Alerts.AddError(this, "Errore in aggiornamento");
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
                await _sharedService.DeleteTeam(id); 
                Alerts.AddSuccess(this, "Team cancellato");
            }
            catch (Exception e)
            {
                Alerts.AddError(this, "Errore nella cancellazione: " + e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
