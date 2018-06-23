using DAFA.Domain.Entities;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Services
{
    public interface IEventWarningService : IBaseService<EventWarning>
    {
        IEnumerable<EventWarning> GetUnsolved();

        IEnumerable<EventWarning> GetUnsolvedByClient(Guid id);

        new EventWarning GetById(Guid id);

        new ValidationResult Update(EventWarning eventWarning);
    }
}
