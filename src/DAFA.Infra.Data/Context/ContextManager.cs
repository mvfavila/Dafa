using DAFA.Infra.Data.Interfaces;
using System.Web;

namespace DAFA.Infra.Data.Context
{
    public class ContextManager : IContextManager
    {
        private const string ContextKey = "ContextManager.Context";

        public DAFAContext GetContext()
        {
            if (HttpContext.Current.Items[ContextKey] == null)
            {
                HttpContext.Current.Items[ContextKey] = new DAFAContext();
            }

            return (DAFAContext)HttpContext.Current.Items[ContextKey];
        }
    }
}
