using DAFA.Application.Interfaces;
using DAFA.Application.ViewModels;
using DAFA.Infra.CrossCutting.MvcFilters;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    [Authorize]
    public class EventTypeController : Controller
    {
        private readonly IEventTypeAppService eventTypeAppService;

        public EventTypeController(IEventTypeAppService eventTypeAppService)
        {
            this.eventTypeAppService = eventTypeAppService;
        }

        // GET: EventTypes
        [ClaimsAuthorize("ViewEventType", "True")]
        public ActionResult Index()
        {
            return View(eventTypeAppService.GetAll());
        }

        // GET: EventType/Create
        [ClaimsAuthorize("CreateEventType", "True")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventType/Create
        [ClaimsAuthorize("CreateEventType", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "EventTypeId,Name,NumberOfDaysToWarning")]
            EventTypeViewModel eventTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = eventTypeAppService.Add(eventTypeViewModel);
                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(eventTypeViewModel);
                }

                // TODO: check if this should be the action to redirect to
                return RedirectToAction(nameof(Index));
            }

            return View(eventTypeViewModel);
        }

        // GET: EventType/Edit/5
        [ClaimsAuthorize("EditEventType", "True")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventTypeViewModel = eventTypeAppService.GetById((Guid)id);
            if (eventTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(eventTypeViewModel);
        }

        // POST: EventType/Edit/5
        [ClaimsAuthorize("EditEventType", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "EventTypeId,Name,NumberOfDaysToWarning,Active")]
            EventTypeViewModel eventTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = eventTypeAppService.Update(eventTypeViewModel);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(eventTypeViewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(eventTypeViewModel);
        }

        // GET: EventType/Details/5
        [ClaimsAuthorize("ViewEventType", "True")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventTypeViewModel = eventTypeAppService.GetById((Guid)id);
            if (eventTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(eventTypeViewModel);
        }

        #region Extension methods

        [HttpGet]
        [ClaimsAuthorize("ViewEventType", "True")]
        public JsonResult GetAll()
        {
            var eventTypeViewModels = eventTypeAppService.GetAll();

            return Json(eventTypeViewModels, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}