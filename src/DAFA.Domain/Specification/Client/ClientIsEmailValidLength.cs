using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.Client
{
    public class ClientIsEmailValidLength : ISpecification<Entities.Client>
    {
        const int EMAIL_MAX_LENGTH = 100;

        public bool IsSatisfiedBy(Entities.Client entity)
        {
            if (entity.Email == null)
                return true;

            return entity.Email.Length <= EMAIL_MAX_LENGTH;
        }
    }
}
