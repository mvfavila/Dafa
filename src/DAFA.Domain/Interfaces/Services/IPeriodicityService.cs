using DAFA.Domain.Entities;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Interfaces.Services
{
    public interface IPeriodicityService : IBaseService<Periodicity>
    {
        IEnumerable<Periodicity> GetActive();

        IEnumerable<Periodicity> Find(Expression<Func<Periodicity, bool>> predicate);

        new ValidationResult Add(Periodicity periodicity);

        new ValidationResult Update(Periodicity periodicity);
    }
}
