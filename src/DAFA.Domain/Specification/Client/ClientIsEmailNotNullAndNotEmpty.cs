using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.Client
{
    public class ClientIsEmailNotNullAndNotEmpty : ISpecification<Entities.Client>
    {
        public bool IsSatisfiedBy(Entities.Client entity)
        {
            return !string.IsNullOrEmpty(entity.Email) && !string.IsNullOrWhiteSpace(entity.Email);
        }
    }
}
