using DAFA.Infra.Data.Context;

namespace DAFA.Infra.Data.Interfaces
{
    public interface IContextManager
    {
        DAFAContext GetContext();
    }
}
