using DAFA.Domain.Interfaces.Specification;

namespace DAFA.Domain.Specification.Client
{
    public class ClientIsPhoneValidLength : ISpecification<Entities.Client>
    {
        const int PHONE_LENGTH = 25;

        public bool IsSatisfiedBy(Entities.Client entity)
        {
            if (entity.Phone == null)
                return true;

            return entity.Phone.Length <= PHONE_LENGTH;
        }
    }
}
