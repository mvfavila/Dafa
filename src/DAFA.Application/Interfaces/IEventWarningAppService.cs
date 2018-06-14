using DAFA.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace DAFA.Application.Interfaces
{
    public interface IEventWarningAppService : IDisposable
    {
        IEnumerable<EventWarningViewModel> GetUnsolved();

        IEnumerable<EventWarningViewModel> GetUnsolvedByClient(Guid id);
    }
}
