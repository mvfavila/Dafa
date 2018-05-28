using DAFA.Domain.Entities;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Repository.ReadOnly
{
    public interface IClientReadOnlyRepository : IBaseReadOnlyRepository<Client>
    {
        IEnumerable<Client> GetActive();
    }
}
