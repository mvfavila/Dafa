using DAFA.Application.Interfaces;
using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using DAFA.Domain.Interfaces.Services;
using DAFA.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace DAFA.Application.AppServices
{
    public class EventAppService : BaseAppService<DAFAContext>, IEventAppService
    {
        private readonly IEventService eventService;

        public EventAppService(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public ValidationAppResult Add(EventViewModel eventViewModel)
        {
            var eventObj = Mapping.EventMapper.FromViewModelToDomain(eventViewModel);

            var result = eventService.Add(eventObj);

            Commit();

            return FromDomainToApplicationResult(result);
        }

        public void Dispose()
        {
            eventService.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEnumerable<EventViewModel> GetActive()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventViewModel> GetAll()
        {
            var events = eventService.GetAll();

            return Mapping.EventMapper.FromDomainToViewModel(events);
        }

        public EventViewModel GetById(Guid id)
        {
            var eventObj = eventService.GetById(id);

            return Mapping.EventMapper.FromDomainToViewModel(eventObj);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValidationAppResult Update(EventViewModel eventViewModel)
        {
            var eventObj = Mapping.EventMapper.FromViewModelToDomain(eventViewModel);

            BeginTransaction();

            var result = eventService.Update(eventObj);

            if (result.IsValid)
                Commit();

            return FromDomainToApplicationResult(result);
        }
    }
}
