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
    public class EventController : Controller
    {
        private readonly IEventAppService eventAppService;

        public EventController(IEventAppService eventAppService)
        {
            this.eventAppService = eventAppService;
        }

        // GET: Events
        [ClaimsAuthorize("ViewEvent", "True")]
        public ActionResult Index()
        {
            return View(eventAppService.GetAll());
        }

        // GET: Event/Create
        [ClaimsAuthorize("CreateEvent", "True")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [ClaimsAuthorize("CreateEvent", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "EventId,Name,NumberOfDaysToWarning")]
            EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = eventAppService.Add(eventViewModel);
                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(eventViewModel);
                }

                // TODO: check if this should be the action to redirect to
                return RedirectToAction(nameof(Index));
            }

            return View(eventViewModel);
        }

        // GET: Event/Edit/5
        [ClaimsAuthorize("EditEvent", "True")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventViewModel = eventAppService.GetById((Guid)id);
            if (eventViewModel == null)
            {
                return HttpNotFound();
            }
            return View(eventViewModel);
        }

        // POST: Event/Edit/5
        [ClaimsAuthorize("EditEvent", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "EventId,Name,NumberOfDaysToWarning,Active")]
            EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = eventAppService.Update(eventViewModel);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(eventViewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(eventViewModel);
        }

        // GET: Event/Details/5
        [ClaimsAuthorize("ViewEvent", "True")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eventViewModel = eventAppService.GetById((Guid)id);
            if (eventViewModel == null)
            {
                return HttpNotFound();
            }
            return View(eventViewModel);
        }
    }
}