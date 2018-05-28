using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using System.Collections.Generic;

namespace DAFA.Application.Mapping
{
    public static class EventTypeMapper
    {
        // EventTypeViewModel
        internal static EventType FromViewModelToDomain(EventTypeViewModel eventTypeViewModel)
        {
            return EventType.FactoryMap(
                eventTypeViewModel.EventTypeId,
                eventTypeViewModel.Name,
                eventTypeViewModel.NumberOfDaysToWarning,
                eventTypeViewModel.Active);
        }

        internal static EventTypeViewModel FromDomainToViewModel(EventType eventType)
        {
            return new EventTypeViewModel
            {
                EventTypeId = eventType.EventTypeId,
                Name = eventType.Name,
                NumberOfDaysToWarning = eventType.NumberOfDaysToWarning,
                Active = eventType.Active
            };
        }

        internal static IEnumerable<EventTypeViewModel> FromDomainToViewModel(IEnumerable<EventType> eventTypes)
        {
            var result = new List<EventTypeViewModel>();

            foreach (var eventType in eventTypes)
            {
                result.Add(FromDomainToViewModel(eventType));
            }

            return result;
        }
    }
}
