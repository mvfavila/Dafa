using DAFA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Interfaces
{
    public interface IEventWarningAppService : IDisposable
    {
        void Add(EventWarningViewModel eventWarningViewModel);

        IEnumerable<EventWarningViewModel> GetUnsolved();

        IEnumerable<EventWarningViewModel> GetUnsolvedByClient(Guid id);
    }
}
