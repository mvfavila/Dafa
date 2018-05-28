using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;
using DAFA.Domain.Validation.EventType;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Services
{
    public class EventTypeService : BaseService<EventType>, IEventTypeService
    {
        private readonly IEventTypeRepository eventTypeRepository;
        private readonly IEventTypeReadOnlyRepository eventTypeReadOnlyRepository;

        public EventTypeService(
            IEventTypeRepository eventTypeRepository,
            IEventTypeReadOnlyRepository eventTypeReadOnlyRepository)
            : base(eventTypeRepository, eventTypeReadOnlyRepository)
        {
            this.eventTypeRepository = eventTypeRepository;
            this.eventTypeReadOnlyRepository = eventTypeReadOnlyRepository;
        }

        public IEnumerable<EventType> Find(Expression<Func<EventType, bool>> predicate)
        {
            return eventTypeReadOnlyRepository.Find(predicate);
        }

        public IEnumerable<EventType> GetActive()
        {
            return eventTypeReadOnlyRepository.GetActive();
        }

        public new IEnumerable<EventType> GetAll()
        {
            return eventTypeReadOnlyRepository.GetAll();
        }

        public new ValidationResult Add(EventType eventType)
        {
            var validationResult = new ValidationResult();

            if (!eventType.IsValid())
            {
                validationResult.AddError(eventType.ValidationResult);
                return validationResult;
            }

            var validator = new EventTypeIsVerifiedForRegistration();
            var validationService = validator.Validate(eventType);
            if (!validationService.IsValid)
            {
                validationResult.AddError(eventType.ValidationResult);
                return validationResult;
            }

            eventTypeRepository.Add(eventType);

            return validationResult;
        }

        public new ValidationResult Update(EventType eventType)
        {
            var validationResult = new ValidationResult();

            if (!eventType.IsValid())
            {
                validationResult.AddError(eventType.ValidationResult);
                return validationResult;
            }

            var validator = new EventTypeIsVerifiedForRegistration();
            var validationService = validator.Validate(eventType);
            if (!validationService.IsValid)
            {
                validationResult.AddError(eventType.ValidationResult);
                return validationResult;
            }

            eventTypeRepository.Update(eventType);

            return validationResult;
        }
    }
}
