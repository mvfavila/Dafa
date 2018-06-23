using DAFA.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Repository.ReadOnly
{
    public interface IEventWarningReadOnlyRepository : IBaseReadOnlyRepository<EventWarning>
    {
        IEnumerable<EventWarning> GetUnsolved();

        IEnumerable<EventWarning> GetUnsolvedByClient(Guid id);

        new EventWarning GetById(Guid id);
    }
}
