using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Interfaces
{
    public interface IEventTypeAppService : IDisposable
    {
        IEnumerable<EventTypeViewModel> GetActive();

        // TODO: paging should be added to method.
        IEnumerable<EventTypeViewModel> GetAll();

        EventTypeViewModel GetById(Guid id);

        ValidationAppResult Add(EventTypeViewModel eventTypeViewModel);

        ValidationAppResult Update(EventTypeViewModel eventTypeViewModel);

        void Remove(Guid id);
    }
}
