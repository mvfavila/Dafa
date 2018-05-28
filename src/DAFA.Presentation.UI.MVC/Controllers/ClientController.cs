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
    public class ClientController : Controller
    {
        private readonly IClientAppService clientAppService;

        public ClientController(IClientAppService clientAppService)
        {
            this.clientAppService = clientAppService;
        }

        // GET: Clients
        [ClaimsAuthorize("ViewClient", "True")]
        public ActionResult Index()
        {
            return View(clientAppService.GetAll());
        }

        // GET: Client/Create
        [ClaimsAuthorize("CreateClient", "True")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [ClaimsAuthorize("CreateClient", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "ClientId,Name,Email,Phone")]
            ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = clientAppService.Add(clientViewModel);
                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(clientViewModel);
                }

                // TODO: check if this should be the action to redirect to
                return RedirectToAction(nameof(Index));
            }

            return View(clientViewModel);
        }

        // GET: Client/Edit/5
        [ClaimsAuthorize("EditClient", "True")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientViewModel = clientAppService.GetById((Guid)id);
            if (clientViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clientViewModel);
        }

        // POST: Client/Edit/5
        [ClaimsAuthorize("EditClient", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "ClientId,Name,Email,Phone,Active")]
            ClientViewModel clientViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = clientAppService.Update(clientViewModel);

                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(clientViewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);
        }

        // GET: Client/Details/5
        [ClaimsAuthorize("ViewClient", "True")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientFieldViewModel = clientAppService.GetById((Guid)id);
            if (clientFieldViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clientFieldViewModel);
        }
    }
}