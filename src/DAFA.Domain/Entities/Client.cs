using DAFA.Domain.Interfaces.Validation;
using System;
using DAFA.Domain.ValueObjects;
using System.Collections.Generic;
using DAFA.Domain.Validation.Client;

namespace DAFA.Domain.Entities
{
    public class Client : ISelfValidator
    {
        private Client() { ValidationResult = new ValidationResult(); }

        public Guid ClientId { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Phone { get; private set; }

        public bool Active { get; private set; }

        public virtual ICollection<Field> Fields { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            var validation = new ClientIsVerifiedForRegistration();
            ValidationResult = validation.Validate(this);

            return ValidationResult.IsValid;
        }

        public static Client FactoryMap(Guid clientId, string name, string email, string phone, bool active)
        {
            return new Client
            {
                ClientId = clientId,
                Name = name,
                Email = email,
                Phone = phone,
                Active = active
            };
        }
    }
}
