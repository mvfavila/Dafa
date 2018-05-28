using System.Web.Mvc;

namespace DAFA.Infra.CrossCutting.Filters
{
    public sealed class DAFAErrorHandler : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            // TODO: log error here

            if(filterContext.Exception != null)
            {
                filterContext.Controller.TempData["ErrorMessage"] =
                    "An error occurred while processing your request. Please contact your system Administrator.";
            }
        }
    }
}
