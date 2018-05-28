using DAFA.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Interfaces.Repository.ReadOnly
{
    public interface IFieldReadOnlyRepository : IBaseReadOnlyRepository<Field>
    {
        IEnumerable<Field> GetActiveByClient(Guid clientId);

        IEnumerable<Field> GetAllByClient(Guid clientId);
    }
}
