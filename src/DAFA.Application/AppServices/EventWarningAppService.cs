using DAFA.Application.Interfaces;
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
        private readonly IEventService eventService;

        public EventWarningAppService(IEventWarningService eventWarningService, IEventService eventService)
        {
            this.eventWarningService = eventWarningService;
            this.eventService = eventService;
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

            var viewModels = Mapping.EventWarningMapper.FromDomainToViewModel(eventWarnings);

            return FillViewModels(viewModels);
        }

        public IEnumerable<EventWarningViewModel> GetUnsolvedByClient(Guid id)
        {
            var eventWarnings = eventWarningService.GetUnsolvedByClient(id);

            var viewModels = Mapping.EventWarningMapper.FromDomainToViewModel(eventWarnings);

            return FillViewModels(viewModels);
        }

        public void Dispose()
        {
            eventWarningService.Dispose();
            GC.SuppressFinalize(this);
        }

        public EventWarningViewModel GetById(Guid id)
        {
            var eventWarning = eventWarningService.GetById(id);

            var eventWarningViewModel = Mapping.EventWarningMapper.FromDomainToViewModel(eventWarning);

            return FillViewModels(eventWarningViewModel);
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

        private IEnumerable<EventWarningViewModel> FillViewModels(IEnumerable<EventWarningViewModel> viewModels)
        {
            var newList = new List<EventWarningViewModel>();
            foreach (var viewModel in viewModels)
            {
                newList.Add(FillViewModels(viewModel));
            }
            return newList;
        }

        private EventWarningViewModel FillViewModels(EventWarningViewModel viewModel)
        {
            var eventObj = eventService.GetById(viewModel.EventId);
            viewModel.Event = Mapping.EventMapper.FromDomainToViewModel(eventObj);
            return viewModel;
        }
    }
}
