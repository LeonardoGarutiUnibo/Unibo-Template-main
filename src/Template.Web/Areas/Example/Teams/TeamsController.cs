using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Template.Services.Shared;
using Template.Web.Infrastructure;
using Template.Web.SignalR;
using Template.Web.SignalR.Hubs.Events;
using System.Linq;


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
                    model.Id = await _sharedService.HandleTeam(model.ToAddOrUpdateTeamCommand());
                    Alerts.AddSuccess(this, "Informazioni aggiornate");
                }
                catch (Exception e)
                {
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
                var members = await _sharedService.QueryTeamMemberUsers(id, false);
                var managers = await _sharedService.QueryTeamMemberUsers(id, true);
        
                if ((members?.Any() ?? false))
                {
                    Alerts.AddError(this, "Impossibile cancellare il team: sono ancora presenti membri.");
                    return RedirectToAction(nameof(Index));
                }
                if ((managers?.Any() ?? false))
                {
                    try{
                        await _sharedService.DeleteTeamMember(managers[0].Id);
                    }
                    catch (Exception e){
                        Alerts.AddError(this, "Errore nella cancellazione del Manager: " + e.Message);
                        return RedirectToAction(nameof(Index));
                    }
                    
                }
                await _sharedService.DeleteTeam(id); 
                Alerts.AddSuccess(this, "Team cancellato con successo.");
            }
            catch (Exception e)
            {
                Alerts.AddError(this, "Errore nella cancellazione: " + e.Message);
            }
        
            return RedirectToAction(nameof(Index));
        }
    }
}
