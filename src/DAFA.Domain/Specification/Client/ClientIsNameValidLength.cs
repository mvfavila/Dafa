using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.Client
{
    public class ClientIsNameValidLength : ISpecification<Entities.Client>
    {
        const int NAME_MAX_LENGTH = 250;

        public bool IsSatisfiedBy(Entities.Client entity)
        {
            if (entity.Name == null)
                return true;

            return entity.Name.Length <= NAME_MAX_LENGTH;
        }
    }
}
