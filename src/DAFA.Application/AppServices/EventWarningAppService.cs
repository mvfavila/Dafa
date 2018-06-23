﻿using DAFA.Application.Interfaces;
using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
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

        public void Add(EventWarningViewModel eventWarningViewModel)
        {
            var model = EventWarning.FactoryAdd(eventWarningViewModel.EventId,
                Mapping.EventMapper.FromViewModelToDomain(eventWarningViewModel.Event));

            eventWarningService.Add(model);
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

        public EventWarningViewModel GetById(Guid id)
        {
            var eventWarning = eventWarningService.GetById(id);

            return Mapping.EventWarningMapper.FromDomainToViewModel(eventWarning);
        }

        public ValidationAppResult Update(EventWarningViewModel eventWarningViewModel)
        {
            var model = EventWarning.FactoryMap(eventWarningViewModel.EventWarningId, eventWarningViewModel.Date,
                eventWarningViewModel.SolvedDate, eventWarningViewModel.Solved, eventWarningViewModel.EventId,
                null);

            var result = eventWarningService.Update(model);

            Commit();

            return FromDomainToApplicationResult(result);
        }
    }
}
