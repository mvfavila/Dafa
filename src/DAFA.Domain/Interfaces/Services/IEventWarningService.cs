using DAFA.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Services
{
    public interface IEventWarningService : IBaseService<EventWarning>
    {
        IEnumerable<EventWarning> GetUnsolved();

        IEnumerable<EventWarning> GetUnsolvedByClient(Guid id);
    }
}
