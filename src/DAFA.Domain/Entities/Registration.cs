using DAFA.Domain.Interfaces.Validation;
using DAFA.Domain.ValueObjects;
using System;

namespace DAFA.Domain.Entities
{
    /// <summary>
    /// Main object of the application.
    /// </summary>
    public class Registration : ISelfValidator
    {
        private Registration() { ValidationResult = new ValidationResult(); }

        private Registration(Guid registrationId, Guid fieldId, Guid eventId, Guid periodicityId, bool active)
        {
            RegistrationId = registrationId;
            FieldId = fieldId;
            EventId = eventId;
            PeriodicityId = periodicityId;
            Active = active;
        }

        public Guid RegistrationId { get; set; }

        public Guid FieldId { get; private set; }

        public virtual Field Field { get; private set; }

        public Guid EventId { get; private set; }

        public virtual Event Event { get; private set; }

        public Guid PeriodicityId { get; private set; }

        public virtual Periodicity Periodicity { get; private set; }

        public bool Active { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            //var validation = new EventIsVerifiedForRegistration();
            //ValidationResult = validation.Validate(this);

            //return ValidationResult.IsValid;

            return true;
        }

        public static Registration FactoryAdd(Guid registrationId, Guid fieldId, Guid eventId, Guid periodicityId)
        {
            const bool ACTIVE = true;

            return new Registration(registrationId, fieldId, eventId, periodicityId, ACTIVE);
        }
    }
}
