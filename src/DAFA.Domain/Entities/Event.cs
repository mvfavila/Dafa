using DAFA.Domain.Interfaces.Validation;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Entities
{
    public class Event : ISelfValidator
    {
        private Event() { ValidationResult = new ValidationResult(); }

        private Event(Guid eventId, string name, string description, DateTime date, Guid fieldId, Guid eventTypeId)
        {
            EventId = eventId;
            Name = name;
            Description = description;
            Date = date;
            FieldId = fieldId;
            EventTypeId = eventTypeId;
            EventWarnings = new List<EventWarning>();
            ValidationResult = new ValidationResult();
        }

        public Guid EventId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public DateTime Date { get; private set; }

        public Guid FieldId { get; private set; }

        public virtual Field Field { get; private set; }

        public Guid EventTypeId { get; private set; }

        public virtual EventType EventType { get; private set; }

        public virtual ICollection<Registration> Registrations { get; private set; }

        public virtual ICollection<EventWarning> EventWarnings { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            //var validation = new EventIsVerifiedForRegistration();
            //ValidationResult = validation.Validate(this);

            //return ValidationResult.IsValid;

            return true;
        }

        public void SetField(Field field)
        {
            Field = field;
        }

        public static Event FactoryMap(
            Guid eventId, string name, string description, DateTime date, Guid fieldId, Guid eventTypeId)
        {
            return new Event(eventId, name, description, date, fieldId, eventTypeId);
        }

        public static Event FactorySeed(
            string eventId, string name, string description, DateTime date, Guid fieldId, Guid eventTypeId)
        {
            return new Event(Guid.Parse(eventId), name, description, date, fieldId, eventTypeId);
        }
    }
}
