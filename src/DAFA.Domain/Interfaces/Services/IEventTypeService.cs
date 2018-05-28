using DAFA.Domain.Entities;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Interfaces.Services
{
    public interface IEventTypeService : IBaseService<EventType>
    {
        IEnumerable<EventType> GetActive();

        IEnumerable<EventType> Find(Expression<Func<EventType, bool>> predicate);

        new ValidationResult Add(EventType eventType);

        new ValidationResult Update(EventType eventType);
    }
}
