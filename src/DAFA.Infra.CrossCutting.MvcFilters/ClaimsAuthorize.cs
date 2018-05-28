using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DAFA.Infra.CrossCutting.MvcFilters
{
    public sealed class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string claimName;
        private readonly string claimValue;

        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
        {
            this.claimName = claimName;
            this.claimValue = claimValue;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == claimName);

            if (claim != null)
            {
                return claim.Value == claimValue;
            }

            return false;
        }
    }
}
