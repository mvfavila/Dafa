using System;

namespace DAFA.Application.Interfaces
{
    public interface IWarningsProcessingAppService : IDisposable
    {
        void ProcessWarnings();
    }
}
