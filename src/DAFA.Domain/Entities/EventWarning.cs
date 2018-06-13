using System;

namespace DAFA.Domain.Entities
{
    /// <summary>
    /// Represents a warning raised by an Event after the minimum number of days to warnings being reached.<br/>
    /// This warnings must be marked as Solved so the system stops warning the administrator about it's existence.
    /// </summary>
    public class EventWarning
    {
        public Guid EventWarningId { get; private set; }

        public DateTime Date { get; private set; }

        public bool Solved { get; private set; }

        public Guid EventId { get; private set; }

        public virtual Event Event { get; private set; }
    }
}
