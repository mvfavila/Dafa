using DAFA.Application.Validation;
using DAFA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Interfaces
{
    public interface IEventAppService : IDisposable
    {
        IEnumerable<EventViewModel> GetActive();

        // TODO: paging should be added to method.
        IEnumerable<EventViewModel> GetAll();

        EventViewModel GetById(Guid id);

        ValidationAppResult Add(EventViewModel eventViewModel);

        ValidationAppResult Update(EventViewModel eventViewModel);

        void Remove(Guid id);

        IEnumerable<EventViewModel> ProcessEventWarnings();
    }
}
