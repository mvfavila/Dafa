using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;

namespace DAFA.Domain.Services
{
    public class MenuService : BaseService<MenuItem>, IMenuService
    {
        private readonly IMenuReadOnlyRepository menuReadOnlyRepository;

        public MenuService(IMenuReadOnlyRepository menuReadOnlyRepository)
            : base(null, menuReadOnlyRepository)
        {
            this.menuReadOnlyRepository = menuReadOnlyRepository;
        }
    }
}
