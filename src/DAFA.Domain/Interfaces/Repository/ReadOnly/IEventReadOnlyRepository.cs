using DAFA.Domain.Entities;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Repository.ReadOnly
{
    public interface IEventReadOnlyRepository : IBaseReadOnlyRepository<Event>
    {
        IEnumerable<Event> GetActive();
    }
}
