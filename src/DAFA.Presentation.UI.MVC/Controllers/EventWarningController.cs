using DAFA.Application.Interfaces;
using DAFA.Infra.CrossCutting.Logging;
using DAFA.Infra.CrossCutting.MvcFilters;
using DAFA.Presentation.UI.MVC.Util;
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

            Log.Info($@"Usuário '{UtilString.AddEmailMask(User.Identity.Name)}' marcou o aviso abaixo como solucionado.
                Nome do evento: {eventWarningViewModel.Event.Name}
                Descrição: {eventWarningViewModel.Event.Description}
                Data: {eventWarningViewModel.Date}");

            eventWarningAppService.Update(eventWarningViewModel);

            return RedirectToAction("Index", "Home");
        }
    }
}