using DAFA.Domain.Interfaces.Specification;
using System;

namespace DAFA.Domain.Specification.Client
{
    public class ClientIsKeyNotNull : ISpecification<Entities.Client>
    {
        public bool IsSatisfiedBy(Entities.Client entity)
        {
            return entity.ClientId != Guid.Empty;
        }
    }
}
