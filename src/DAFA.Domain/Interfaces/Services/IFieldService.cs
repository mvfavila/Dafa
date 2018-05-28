using DAFA.Domain.Entities;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Interfaces.Services
{
    public interface IFieldService : IBaseService<Field>
    {
        IEnumerable<Field> GetActiveByClient(Guid clientId);

        IEnumerable<Field> GetAllByClient(Guid clientId);

        IEnumerable<Field> Find(Expression<Func<Field, bool>> predicate);

        new ValidationResult Add(Field field);

        new ValidationResult Update(Field field);
    }
}
