using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.EventWarning
{
    public class EventWarningIsValidSolvedState : ISpecification<Entities.EventWarning>
    {
        public bool IsSatisfiedBy(Entities.EventWarning entity)
        {
            if (entity.Solved && entity.SolvedDate == null)
                return false;

            if (!entity.Solved && entity.SolvedDate != null)
                return false;

            return true;
        }
    }
}
