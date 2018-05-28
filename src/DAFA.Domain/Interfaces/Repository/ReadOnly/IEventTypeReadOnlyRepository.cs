using DAFA.Domain.Entities;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Repository.ReadOnly
{
    public interface IEventTypeReadOnlyRepository : IBaseReadOnlyRepository<EventType>
    {
        IEnumerable<EventType> GetActive();
    }
}
