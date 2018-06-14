using DAFA.Application.Interfaces;
using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using DAFA.Domain.Interfaces.Services;
using DAFA.Infra.CrossCutting.Identity.Configuration;
using DAFA.Infra.Data.Context;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAFA.Application.AppServices
{
    public class EventAppService : BaseAppService<DAFAContext>, IEventAppService
    {
        private const string DATE_FORMAT = "dd/MM/yyyy";
        private readonly IEventService eventService;
        private readonly IEventWarningService eventWarningService;

        public EventAppService(IEventService eventService, IEventWarningService eventWarningService)
        {
            this.eventService = eventService;
            this.eventWarningService = eventWarningService;
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

        public void ProcessEventWarnings()
        {
            var overdueEvents = eventService.GetOverdueEvents();

            if (overdueEvents.Any())
                AddEventWarnings(overdueEvents);

            var eventWarnings = eventWarningService.GetUnsolved();

            var builder = new StringBuilder();
            foreach (var ew in eventWarnings)
            {
                var line = $@"Jazida '{ew.Event.Field.Name}'.
                    O evento '{ew.Event.Name}' vence no dia {ew.Date.ToString(DATE_FORMAT)}";
                builder.Append(line).Append("<br/>");
            }

            using (var emailService = new EmailService())
            {
                emailService.SendAsync(new IdentityMessage
                {
                    Destination = "mvfavila@gmail.com",
                    Subject = $"DAFA - Alertas {DateTime.Today.ToString(DATE_FORMAT)}",
                    Body = builder.ToString()
                });
            }
        }

        private void AddEventWarnings(IEnumerable<Event> overdueEvents)
        {
            BeginTransaction();

            foreach (var e in overdueEvents)
            {
                eventWarningService.Add(EventWarning.CreateFromEvent(e));
            }

            Commit();
        }
    }
}
