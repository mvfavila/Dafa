using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.EventWarning
{
    public class EventWarningIsSolvedDateValid : ISpecification<Entities.EventWarning>
    {
        public bool IsSatisfiedBy(Entities.EventWarning entity)
        {
            if (entity.SolvedDate == null)
                return true;

            if (entity.SolvedDate >= entity.Date)
                return true;

            return false;
        }
    }
}
