using DAFA.Infra.CrossCutting.Identity.Configuration;
using DAFA.Infra.CrossCutting.MvcFilters;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    [ClaimsAuthorize("AdmUsers", "True")]
    public class UsersAdminController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Gets the list of Roles
            ViewBag.RoleId = new SelectList(await roleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
    }
}