using DAFA.Application.Interfaces;
using DAFA.Application.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventWarningAppService eventWarningAppService;

        public HomeController(IEventWarningAppService eventWarningAppService)
        {
            this.eventWarningAppService = eventWarningAppService;
        }

        public ActionResult Index()
        {
            var loggedId = User.Identity.GetUserId();

            if (loggedId == null || !Guid.TryParse(loggedId, out Guid userId))
            {
                return View(new List<EventWarningViewModel>());
            }

            var eventWarnings = eventWarningAppService.GetUnsolved();
            return View(eventWarnings);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}