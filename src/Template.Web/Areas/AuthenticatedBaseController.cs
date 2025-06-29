using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using Template.Web.Infrastructure;

namespace Template.Web.Areas
{
    [Authorize]
    [Alerts]
    [ModelStateToTempData]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public partial class AuthenticatedBaseController : Controller
    {
        public AuthenticatedBaseController() { }

        protected IdentitaViewModel Identita
        {
            get
            {
                return (IdentitaViewModel)ViewData[IdentitaViewModel.VIEWDATA_IDENTITACORRENTE_KEY];
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
    {
        if (context.HttpContext != null && context.HttpContext.User != null && context.HttpContext.User.Identity.IsAuthenticated)
        {
            var emailClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role || x.Type == "role");

            foreach (var claim in context.HttpContext.User.Claims)
{
    Console.WriteLine($"{claim.Type} = {claim.Value}");
}
            ViewData[IdentitaViewModel.VIEWDATA_IDENTITACORRENTE_KEY] = new IdentitaViewModel
            {
                EmailUtenteCorrente = emailClaim?.Value,
                Role = roleClaim?.Value
            };
        }
        else
        {
            HttpContext.SignOutAsync();
            this.SignOut();

            context.Result = new RedirectResult(context.HttpContext.Request.GetEncodedUrl());
            Alerts.AddError(this, "L'utente non possiede i diritti per visualizzare la risorsa richiesta");
        }

        base.OnActionExecuting(context);
    }
    catch (Exception)
    {
        throw;
    }
        }
    }
}
