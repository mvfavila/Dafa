using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.Client
{
    public class ClientIsPhoneNotNullAndNotEmpty : ISpecification<Entities.Client>
    {
        public bool IsSatisfiedBy(Entities.Client entity)
        {
            return !string.IsNullOrEmpty(entity.Phone) && !string.IsNullOrWhiteSpace(entity.Phone);
        }
    }
}
