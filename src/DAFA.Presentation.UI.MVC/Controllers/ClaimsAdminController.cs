using Microsoft.AspNet.Identity;
using DAFA.Infra.CrossCutting.Identity;
using DAFA.Infra.CrossCutting.Identity.Configuration;
using DAFA.Infra.CrossCutting.Identity.Context;
using DAFA.Infra.CrossCutting.MvcFilters;
using DAFA.Presentation.UI.MVC.ViewModels.Claims;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    [ClaimsAuthorize("AdmClaims", "True")]
    public class ClaimsAdminController : Controller
    {
        private readonly AppDbContext appDbContext;

        private readonly ApplicationUserManager userManager;

        public ClaimsAdminController(ApplicationUserManager userManager, AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            this.userManager = userManager;
        }

        // GET: ClaimsAdmin
        public ActionResult Index()
        {
            return View(appDbContext.Claims.ToList());
        }

        // GET: ClaimsAdmin/SetUserClaim
        public ActionResult SetUserClaim(string id)
        {
            ViewBag.Type = new SelectList
                (
                    appDbContext.Claims.ToList(),
                    "Name",
                    "Name"
                );

            ViewBag.User = userManager.FindById(id);

            return View();
        }

        // POST: ClaimsAdmin/SetUserClaim
        [HttpPost]
        public ActionResult SetUserClaim(ClaimViewModel claim, string id)
        {
            try
            {
                userManager.AddClaimAsync(id, new Claim(claim.Type, claim.Value));

                return RedirectToAction("Details", "UsersAdmin", new { id = id });
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: ClaimsAdmin/CreateClaim
        public ActionResult CreateClaim()
        {
            return View();
        }

        // POST: ClaimsAdmin/CreateClaim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClaim(
            [Bind(Include = "Id,Name")]
            Claims claim)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    appDbContext.Claims.Add(claim);
                    appDbContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}