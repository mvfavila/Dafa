using DAFA.Infra.Data.Interfaces;

namespace DAFA.Application.Interfaces
{
    public interface IBaseAppService<TContext> where TContext : IDbContext
    {
        void BeginTransaction();
        void Commit();
    }
}
