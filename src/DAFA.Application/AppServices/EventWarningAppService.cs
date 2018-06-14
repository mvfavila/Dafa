using DAFA.Application.Interfaces;
using DAFA.Application.ViewModels;
using DAFA.Domain.Interfaces.Services;
using DAFA.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace DAFA.Application.AppServices
{
    public class EventWarningAppService : BaseAppService<DAFAContext>, IEventWarningAppService
    {
        private readonly IEventWarningService eventWarningService;

        public EventWarningAppService(IEventWarningService eventWarningService)
        {
            this.eventWarningService = eventWarningService;
        }

        public IEnumerable<EventWarningViewModel> GetUnsolved()
        {
            var eventWarnings = eventWarningService.GetUnsolved();

            return Mapping.EventWarningMapper.FromDomainToViewModel(eventWarnings);
        }

        public IEnumerable<EventWarningViewModel> GetUnsolvedByClient(Guid id)
        {
            var eventWarnings = eventWarningService.GetUnsolvedByClient(id);

            return Mapping.EventWarningMapper.FromDomainToViewModel(eventWarnings);
        }

        public void Dispose()
        {
            eventWarningService.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
