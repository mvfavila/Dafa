using DAFA.Domain.Interfaces.Validation;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Entities
{
    public class EventType : ISelfValidator
    {
        private EventType() { ValidationResult = new ValidationResult(); }

        public Guid EventTypeId { get; private set; }

        public string Name { get; private set; }

        public int NumberOfDaysToWarning { get; set; }

        public bool Active { get; private set; }

        public virtual ICollection<Event> Events { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            //var validation = new EventTypeIsVerifiedForRegistration();
            //ValidationResult = validation.Validate(this);

            //return ValidationResult.IsValid;

            return true;
        }

        private EventType(Guid eventTypeId, string name, int numberOfDaysToWarning, bool active)
        {
            EventTypeId = eventTypeId;
            Name = name;
            NumberOfDaysToWarning = numberOfDaysToWarning;
            Active = active;
        }

        public static EventType FactoryMap(Guid eventTypeId, string name, int numberOfDaysToWarning, bool active)
        {
            return new EventType(eventTypeId, name, numberOfDaysToWarning, active);
        }

        public static EventType FactorySeed(string eventTypeId, string name, int numberOfDaysToWarning, bool active)
        {
            return new EventType(Guid.Parse(eventTypeId), name, numberOfDaysToWarning, active);
        }
    }
}
