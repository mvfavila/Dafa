using DAFA.Domain.Entities;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Repository.ReadOnly
{
    public interface IPeriodicityReadOnlyRepository : IBaseReadOnlyRepository<Periodicity>
    {
        IEnumerable<Periodicity> GetActive();
    }
}
