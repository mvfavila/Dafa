using DAFA.Infra.CrossCutting.Filters;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new DAFAErrorHandler());
        }
    }
}
