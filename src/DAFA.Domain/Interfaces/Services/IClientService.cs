using DAFA.Domain.Entities;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Interfaces.Services
{
    public interface IClientService : IBaseService<Client>
    {
        IEnumerable<Client> GetActive();

        IEnumerable<Client> Find(Expression<Func<Client, bool>> predicate);

        new ValidationResult Add(Client client);

        new ValidationResult Update(Client client);
    }
}
