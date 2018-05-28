using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DAFA.Infra.CrossCutting.Identity.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
            : base("DAFAConnection", throwIfV1Schema: false)
        {
        }

        public IDbSet<Client> Client { get; set; }

        public IDbSet<Claims> Claims { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}
