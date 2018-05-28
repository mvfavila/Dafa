using Microsoft.AspNet.Identity;
using DAFA.Application.Interfaces;
using DAFA.Application.ViewModels;
using DAFA.Infra.CrossCutting.Identity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC.Controllers
{
    public class NavigationController : Controller
    {
        private readonly ApplicationUserManager userManager;
        private readonly IMenuAppService menuAppService;

        public NavigationController(ApplicationUserManager userManager, IMenuAppService menuAppService)
        {
            this.userManager = userManager;
            this.menuAppService = menuAppService;
        }

        [ChildActionOnly]
        public PartialViewResult GetMenuForUser()
        {
            var loggedId = User.Identity.GetUserId();

            if(loggedId == null)
                return PartialView("_Navigation", new List<MenuItemViewModel>());

            var claims = userManager.GetClaims(loggedId)
                .Select(c => new { c.Type, c.Value })
                .AsEnumerable()
                .Select(c => new Tuple<string, string>(c.Type, c.Value))
                .ToList();

            var model = GetMenu(claims);

            return PartialView("_Navigation", model);
        }

        private IEnumerable<MenuItemViewModel> GetMenu(List<Tuple<string, string>> claims)
        {
            return menuAppService.GetByClaims(claims);
        }
    }
}