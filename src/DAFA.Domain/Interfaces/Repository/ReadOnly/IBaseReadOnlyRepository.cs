using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Interfaces.Repository.ReadOnly
{
    public interface IBaseReadOnlyRepository<TEntity> : IDisposable where TEntity : class
    {
        // TODO: paging should be added to method.
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(Guid id);
    }
}
