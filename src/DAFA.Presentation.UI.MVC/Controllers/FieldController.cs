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
    public class FieldController : Controller
    {
        private readonly IFieldAppService fieldAppService;
        private readonly IClientAppService clientAppService;

        public FieldController(IFieldAppService fieldAppService, IClientAppService clientAppService)
        {
            this.fieldAppService = fieldAppService;
            this.clientAppService = clientAppService;
        }

        // GET: Field
        public ActionResult Index()
        {
            return View();
        }

        // GET: Field/Create
        [ClaimsAuthorize("CreateField", "True")]
        public ActionResult Create(Guid? clientId)
        {
            if (clientId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var clientViewModel = clientAppService.GetById((Guid)clientId);
            if (clientViewModel == null)
            {
                return HttpNotFound();
            }

            var fieldViewModel = new FieldViewModel
            {
                ClientId = clientViewModel.ClientId
            };

            return View(fieldViewModel);
        }

        // POST: Field/Create
        [ClaimsAuthorize("CreateField", "True")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind(Include = "FieldId,Name,ClientId")]
            FieldViewModel fieldViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = fieldAppService.Add(fieldViewModel);
                if (!result.IsValid)
                {
                    foreach (var validationAppError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, validationAppError.Message);
                    }
                    return View(fieldViewModel);
                }

                // TODO: check if this should be the action to redirect to
                return RedirectToAction("Details", "Client", new { id = fieldViewModel.ClientId });
            }

            return View(fieldViewModel);
        }

        // GET: Field/Details/5
        [ClaimsAuthorize("ViewField", "True")]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fieldViewModel = fieldAppService.GetById((Guid)id);
            if (fieldViewModel == null)
            {
                return HttpNotFound();
            }
            return View(fieldViewModel);
        }
    }
}