using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.Client
{
    public class ClientIsNameNotNullAndNotEmpty : ISpecification<Entities.Client>
    {
        public bool IsSatisfiedBy(Entities.Client entity)
        {
            return !string.IsNullOrEmpty(entity.Name) && !string.IsNullOrWhiteSpace(entity.Name);
        }
    }
}
