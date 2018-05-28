using System;

namespace DAFA.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);

        void Update(TEntity obj);

        void Delete(Guid id);
    }
}
