using DAFA.Domain.Interfaces.Validation;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Entities
{
    public class Periodicity : ISelfValidator
    {
        private Periodicity() { ValidationResult = new ValidationResult(); }

        public Guid PeriodicityId { get; private set; }

        public string Description { get; private set; }

        public int Quantity { get; private set; }

        public Unit Unit { get; private set; }

        public bool Active { get; private set; }

        public virtual ICollection<Registration> Registrations { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            return true;
        }

        public static Periodicity FactoryMap(Guid periodicityId, string description, int quantity, Unit unit,
            bool active)
        {
            return new Periodicity
            {
                PeriodicityId = periodicityId,
                Description = description,
                Quantity = quantity,
                Unit = unit,
                Active = active
            };
        }
    }
}
