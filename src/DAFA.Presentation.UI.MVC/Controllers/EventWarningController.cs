using DAFA.Application.Interfaces;
using DAFA.Infra.CrossCutting.MvcFilters;
using System;
using System.Net;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    public class EventWarningController : Controller
    {
        private readonly IEventAppService eventAppService;
        private readonly IEventWarningAppService eventWarningAppService;

        public EventWarningController(IEventAppService eventAppService, IEventWarningAppService eventWarningAppService)
        {
            this.eventAppService = eventAppService;
            this.eventWarningAppService = eventWarningAppService;
        }

        // GET: EventWarning/Solve
        [ClaimsAuthorize("SolveEventWarning", "True")]
        public ActionResult Solve(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventWarningViewModel = eventWarningAppService.GetById((Guid)id);
            if (eventWarningViewModel == null)
            {
                return HttpNotFound();
            }

            eventWarningViewModel.Solved = true;
            eventWarningViewModel.SolvedDate = DateTime.Now;

            eventWarningAppService.Update(eventWarningViewModel);

            return RedirectToAction("Index", "Home");
        }
    }
}