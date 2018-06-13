using DAFA.Domain.Entities;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Interfaces.Services
{
    public interface IEventService : IBaseService<Event>
    {
        IEnumerable<Event> GetActive();

        IEnumerable<Event> Find(Expression<Func<Event, bool>> predicate);

        new ValidationResult Add(Event eventObj);

        new ValidationResult Update(Event eventObj);

        IEnumerable<Event> GetOverdueEvents();
    }
}
