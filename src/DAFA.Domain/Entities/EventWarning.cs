using DAFA.Domain.Interfaces.Validation;
using DAFA.Domain.Validation.EventWarning;
using DAFA.Domain.ValueObjects;
using System;

namespace DAFA.Domain.Entities
{
    /// <summary>
    /// Represents a warning raised by an Event after the minimum number of days to warnings being reached.<br/>
    /// This warnings must be marked as Solved so the system stops warning the administrator about it's existence.
    /// </summary>
    public class EventWarning : ISelfValidator
    {
        private EventWarning()
        {
            EventWarningId = Guid.NewGuid();
            Date = DateTime.Now;
            Solved = false;
            SolvedDate = null;
            ValidationResult = new ValidationResult();
        }

        public Guid EventWarningId { get; private set; }

        public DateTime Date { get; private set; }

        public DateTime? SolvedDate { get; private set; }

        public bool Solved { get; private set; }

        public Guid EventId { get; private set; }

        public virtual Event Event { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            var validation = new EventWarningIsVerifiedForRegistration();
            ValidationResult = validation.Validate(this);

            return ValidationResult.IsValid;
        }

        public void SetEvent(Event e)
        {
            Event = e;
        }

        public static EventWarning CreateFromEvent(Event e)
        {
            return new EventWarning
            {
                EventId = e.EventId,
                Event = e
            };
        }

        public static EventWarning FactoryAdd(Guid eventId, Event e)
        {
            return new EventWarning
            {
                EventId = eventId,
                Event = Event.FactoryMap(e.EventId, e.Name, e.Description, e.Date, e.FieldId, e.EventTypeId)
            };
        }

        public static EventWarning FactoryMap(Guid eventWarningId, DateTime date, DateTime? solvedDate, bool solved,
            Guid eventId, Event e)
        {
            return new EventWarning
            {
                EventWarningId = eventWarningId,
                Date = date,
                SolvedDate = solvedDate,
                Solved = solved,
                EventId = eventId,
                Event = e != null
                    ? Event.FactoryMap(e.EventId, e.Name, e.Description, e.Date, e.FieldId, e.EventTypeId) : null
            };
        }
    }
}
