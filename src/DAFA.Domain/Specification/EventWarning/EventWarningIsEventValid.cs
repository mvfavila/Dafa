using DAFA.Domain.Interfaces.Specification;
using System;

namespace DAFA.Domain.Specification.EventWarning
{
    public class EventWarningIsEventValid : ISpecification<Entities.EventWarning>
    {
        public bool IsSatisfiedBy(Entities.EventWarning entity)
        {
            return entity.EventId != Guid.Empty;
        }
    }
}
