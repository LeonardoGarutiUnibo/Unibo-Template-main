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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var absenceQuery = new AbsenceEventsIndexQuery
            {
                UserId = new List<Guid> { userId }
            };

            var absenceEventsDto = await _sharedService.Query(absenceQuery);

            var model = new UserIndexViewModel
            {
                AbsenceEvents = absenceEventsDto.AbsenceEvents.ToList()
            };

            return View(model);
        }
        [HttpPost]
        public virtual async Task<IActionResult> Delete([FromBody] DeleteRequestModel model)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (model == null || model.Id == Guid.Empty)
                return BadRequest("ID non valido.");

            var success = await _sharedService.DeleteAbsenceEventAsync(model.Id);

            if (success)
                return Ok();
            else
                return BadRequest("Errore durante la cancellazione.");
        }
        
        public class DeleteRequestModel
        {
            public Guid Id { get; set; }
        }
    }
}
