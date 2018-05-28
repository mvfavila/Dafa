﻿using DAFA.Application.ViewModels;
using DAFA.Domain.Entities;
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
                eventViewModel.EventTypeId
                );
        }

        internal static EventViewModel FromDomainToViewModel(Event eventObj)
        {
            return new EventViewModel
            {
                EventId = eventObj.EventId,
                Name = eventObj.Name,
                Description = eventObj.Description,
                Date = eventObj.Date,
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
