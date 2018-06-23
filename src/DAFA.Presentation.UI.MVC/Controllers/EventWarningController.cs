using DAFA.Application.Interfaces;
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
    }
}