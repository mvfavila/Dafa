using DAFA.Application.Interfaces;
using DAFA.Application.ViewModels;
using DAFA.Infra.CrossCutting.MvcFilters;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using System.Net;
using System.Linq;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    [Authorize]
    public class PeriodicityController : Controller
    {
        private readonly IPeriodicityAppService periodicityAppService;

        public PeriodicityController(IPeriodicityAppService periodicityAppService)
        {
            this.periodicityAppService = periodicityAppService;
        }

        // GET: Periodicity
        [ClaimsAuthorize("ViewPeriodicity", "True")]
        public ActionResult Index()
        {
            return View(periodicityAppService.GetAll());
        }

        // GET: Periodicity/Create
        [ClaimsAuthorize("CreatePeriodicity", "True")]
        public ActionResult Create()
        {
            var units = periodicityAppService.GetUnits();
            ViewBag.Unit = ToSelecetItemList(units);

            return View();
        }

        // POST: Periodicity/Create
        [ClaimsAuthorize("CreatePeriodicity", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "PeriodicityId,Description,Quantity,Unit")]
            PeriodicityViewModel periodicityViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = periodicityAppService.Add(periodicityViewModel);
                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(periodicityViewModel);
                }

                // TODO: check if this should be the action to redirect to
                return RedirectToAction(nameof(Index));
            }

            return View(periodicityViewModel);
        }

        // GET: Periodicity/Edit/5
        [ClaimsAuthorize("EditPeriodicity", "True")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var periodicityViewModel = periodicityAppService.GetById((Guid)id);
            if (periodicityViewModel == null)
            {
                return HttpNotFound();
            }

            var units = periodicityAppService.GetUnits();
            ViewBag.Unit = ToSelecetItemList(units);

            return View(periodicityViewModel);
        }

        // POST: Periodicity/Edit/5
        [ClaimsAuthorize("EditPeriodicity", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "PeriodicityId,Description,Quantity,Unit,Active")]
            PeriodicityViewModel periodicityViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = periodicityAppService.Update(periodicityViewModel);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(periodicityViewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(periodicityViewModel);
        }

        // GET: Periodicity/Details/5
        [ClaimsAuthorize("ViewPeriodicity", "True")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var periodicityViewModel = periodicityAppService.GetById((Guid)id);
            if (periodicityViewModel == null)
            {
                return HttpNotFound();
            }
            return View(periodicityViewModel);
        }

        private static IEnumerable<SelectListItem> ToSelecetItemList(IEnumerable<UnitViewModel> units)
        {
            return new SelectList(units, "Value", "Description");
        }
    }
}