using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using System.Collections.Generic;

namespace DAFA.Application.Mapping
{
    public static class EventWarningMapper
    {
        // EventWarningViewModel

        internal static EventWarningViewModel FromDomainToViewModel(EventWarning eventWarning)
        {
            return new EventWarningViewModel
            {
                EventWarningId = eventWarning.EventWarningId,
                Date = eventWarning.Date,
                Solved = eventWarning.Solved,
                EventId = eventWarning.EventId,
                Event = EventMapper.FromDomainToViewModel(eventWarning.Event)
            };
        }

        internal static IEnumerable<EventWarningViewModel> FromDomainToViewModel(
            IEnumerable<EventWarning> eventWarnings)
        {
            var viewModels = new List<EventWarningViewModel>();
            foreach (var model in eventWarnings)
            {
                viewModels.Add(FromDomainToViewModel(model));
            }
            return viewModels;
        }
    }
}
