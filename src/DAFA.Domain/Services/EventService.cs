using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;
using DAFA.Domain.Validation.Event;
using DAFA.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAFA.Domain.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private readonly IEventRepository eventRepository;
        private readonly IEventReadOnlyRepository eventReadOnlyRepository;

        public EventService(
            IEventRepository eventRepository,
            IEventReadOnlyRepository eventReadOnlyRepository)
            : base(eventRepository, eventReadOnlyRepository)
        {
            this.eventRepository = eventRepository;
            this.eventReadOnlyRepository = eventReadOnlyRepository;
        }

        public IEnumerable<Event> Find(Expression<Func<Event, bool>> predicate)
        {
            return eventReadOnlyRepository.Find(predicate);
        }

        public IEnumerable<Event> GetActive()
        {
            return eventReadOnlyRepository.GetActive();
        }

        public new IEnumerable<Event> GetAll()
        {
            return eventReadOnlyRepository.GetAll();
        }

        public new ValidationResult Add(Event eventObj)
        {
            var validationResult = new ValidationResult();

            if (!eventObj.IsValid())
            {
                validationResult.AddError(eventObj.ValidationResult);
                return validationResult;
            }

            var validator = new EventIsVerifiedForRegistration();
            var validationService = validator.Validate(eventObj);
            if (!validationService.IsValid)
            {
                validationResult.AddError(eventObj.ValidationResult);
                return validationResult;
            }

            eventRepository.Add(eventObj);

            return validationResult;
        }

        public new ValidationResult Update(Event eventObj)
        {
            var validationResult = new ValidationResult();

            if (!eventObj.IsValid())
            {
                validationResult.AddError(eventObj.ValidationResult);
                return validationResult;
            }

            var validator = new EventIsVerifiedForRegistration();
            var validationService = validator.Validate(eventObj);
            if (!validationService.IsValid)
            {
                validationResult.AddError(eventObj.ValidationResult);
                return validationResult;
            }

            eventRepository.Update(eventObj);

            return validationResult;
        }
    }
}
