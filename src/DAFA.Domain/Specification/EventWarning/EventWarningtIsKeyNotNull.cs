using DAFA.Domain.Interfaces.Specification;
using System;

namespace DAFA.Domain.Specification.EventWarning
{
    public class EventWarningtIsKeyNotNull : ISpecification<Entities.EventWarning>
    {
        public bool IsSatisfiedBy(Entities.EventWarning entity)
        {
            return entity.EventWarningId != Guid.Empty;
        }
    }
}
