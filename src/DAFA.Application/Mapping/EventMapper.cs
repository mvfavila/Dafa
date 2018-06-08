using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Mapping
{
    public static class EventMapper
    {
        // EventViewModel
        internal static Event FromViewModelToDomain(EventViewModel eventViewModel)
        {
            return Event.FactoryMap(
                eventViewModel.EventId,
                eventViewModel.Name,
                eventViewModel.Description,
                eventViewModel.Date,
                eventViewModel.FieldId,
                eventViewModel.EventTypeId
                );
        }

        internal static IList<Event> FromViewModelToDomain(IList<EventViewModel> eventViewModels)
        {
            var events = new List<Event>();

            foreach (var ev in eventViewModels)
            {
                events.Add(FromViewModelToDomain(ev));
            }

            return events;
        }

        internal static EventViewModel FromDomainToViewModel(Event eventObj)
        {
            return new EventViewModel
            {
                EventId = eventObj.EventId,
                Name = eventObj.Name,
                Description = eventObj.Description,
                Date = eventObj.Date,
                FieldId = eventObj.FieldId,
                EventTypeId = eventObj.EventTypeId
            };
        }

        internal static IEnumerable<EventViewModel> FromDomainToViewModel(IEnumerable<Event> eventObjs)
        {
            var result = new List<EventViewModel>();

            foreach (var eventObj in eventObjs)
            {
                result.Add(FromDomainToViewModel(eventObj));
            }

            return result;
        }
    }
}
