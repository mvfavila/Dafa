using DAFA.Application.Interfaces;
using DAFA.Application.Mapping;
using DAFA.Application.ViewModels;
using DAFA.Domain.Interfaces.Services;
using DAFA.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAFA.Application.AppServices
{
    public class MenuAppService : BaseAppService<DAFAContext>, IMenuAppService
    {
        private readonly IMenuService menuService;

        public MenuAppService(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        public IEnumerable<MenuItemViewModel> GetByClaims(List<Tuple<string, string>> claims)
        {
            var menu = menuService.GetAll()
                .Where(m => claims.Any(t => t.Item1 == m.ClaimType && t.Item2 == m.ClaimValue));

            return MenuMapper.FromDomainToViewModel(menu);
        }

        public void Dispose()
        {
            menuService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
