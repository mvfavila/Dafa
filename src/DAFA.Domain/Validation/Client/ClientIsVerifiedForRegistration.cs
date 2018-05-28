using DAFA.Domain.Specification.Client;
using DAFA.Domain.Validation.Base;

namespace DAFA.Domain.Validation.Client
{
    public class ClientIsVerifiedForRegistration : BaseSupervisor<Entities.Client>
    {
        public ClientIsVerifiedForRegistration()
        {
            var isKeyNotNull = new ClientIsKeyNotNull();
            var isNameNotNullAndNotEmpty = new ClientIsNameNotNullAndNotEmpty();
            var isNameValidLength = new ClientIsNameValidLength();
            var isEmailNotNullAndNotEmpty = new ClientIsEmailNotNullAndNotEmpty();
            var isEmailValidLength = new ClientIsEmailValidLength();
            var isPhoneNotNullAndNotEmpty = new ClientIsPhoneNotNullAndNotEmpty();
            var isPhoneValidLength = new ClientIsPhoneValidLength();

            base.AddRule("IsKeyNotNull", new Rule<Entities.Client>(isKeyNotNull,
                $"{nameof(Entities.Client.ClientId)} is required"));

            base.AddRule("IsNameNotNullAndNotEmpty", new Rule<Entities.Client>(isNameNotNullAndNotEmpty,
                $"{nameof(Entities.Client.Name)} is required"));

            base.AddRule("IsNameValidLength", new Rule<Entities.Client>(isNameValidLength,
                $"{nameof(Entities.Client.Name)} can not have more than 250 chars"));

            base.AddRule("IsEmailNotNullAndNotEmpty", new Rule<Entities.Client>(isEmailNotNullAndNotEmpty,
                $"{nameof(Entities.Client.Email)} is required"));

            base.AddRule("IsEmailValidLength", new Rule<Entities.Client>(isEmailValidLength,
                $"{nameof(Entities.Client.Email)} can not have more than 100 chars"));

            base.AddRule("IsPhoneNotNullAndNotEmpty", new Rule<Entities.Client>(isPhoneNotNullAndNotEmpty,
                $"{nameof(Entities.Client.Phone)} is required"));

            base.AddRule("IsPhoneValidLength", new Rule<Entities.Client>(isPhoneValidLength,
                $"{nameof(Entities.Client.Phone)} can not have more than 25 chars"));
        }
    }
}
