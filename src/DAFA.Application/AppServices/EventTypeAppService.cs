using System;
using System.Collections.Generic;
using DAFA.Application.Interfaces;
using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using DAFA.Infra.Data.Context;
using DAFA.Domain.Interfaces.Services;

namespace DAFA.Application.AppServices
{
    public class EventTypeAppService : BaseAppService<DAFAContext>, IEventTypeAppService
    {
        private readonly IEventTypeService eventTypeService;

        public EventTypeAppService(IEventTypeService eventTypeService)
        {
            this.eventTypeService = eventTypeService;
        }

        public ValidationAppResult Add(EventTypeViewModel eventTypeViewModel)
        {
            var eventType = Mapping.EventTypeMapper.FromViewModelToDomain(eventTypeViewModel);

            var result = eventTypeService.Add(eventType);

            Commit();

            return FromDomainToApplicationResult(result);
        }

        public void Dispose()
        {
            eventTypeService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<EventTypeViewModel> GetActive()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventTypeViewModel> GetAll()
        {
            var eventTypes = eventTypeService.GetAll();

            return Mapping.EventTypeMapper.FromDomainToViewModel(eventTypes);
        }

        public EventTypeViewModel GetById(Guid id)
        {
            var eventType = eventTypeService.GetById(id);

            return Mapping.EventTypeMapper.FromDomainToViewModel(eventType);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValidationAppResult Update(EventTypeViewModel eventTypeViewModel)
        {
            var eventType = Mapping.EventTypeMapper.FromViewModelToDomain(eventTypeViewModel);

            BeginTransaction();

            var result = eventTypeService.Update(eventType);

            if (result.IsValid)
                Commit();

            return FromDomainToApplicationResult(result);
        }
    }
}
