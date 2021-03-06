﻿using DAFA.Domain.Interfaces.Validation;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace DAFA.Domain.Entities
{
    public class Field : ISelfValidator
    {
        private Field()
        {
            Events = new List<Event>();
            Registrations = new List<Registration>();
            ValidationResult = new ValidationResult();
        }

        public Guid FieldId { get; private set; }

        public string Name { get; private set; }

        public bool Active { get; private set; }

        public Guid ClientId { get; private set; }

        public virtual Client Client { get; private set; }

        public virtual ICollection<Event> Events { get; private set; }

        public virtual ICollection<Registration> Registrations { get; private set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid()
        {
            //var validation = new FieldIsVerifiedForRegistration();
            //ValidationResult = validation.Validate(this);

            //return ValidationResult.IsValid;

            return true;
        }

        public static Field FactoryMap(Guid fieldId, string name, IList<Event> events, Guid clientId, bool active)
        {
            return new Field
            {
                FieldId = fieldId,
                Name = name,
                Events = events,
                ClientId = clientId,
                Active = active
            };
        }

        public static Field FactorySeed(string fieldId, string name, Guid clientId, bool active)
        {
            return new Field
            {
                FieldId = Guid.Parse(fieldId),
                Name = name,
                ClientId = clientId,
                Active = active
            };
        }
    }
}
