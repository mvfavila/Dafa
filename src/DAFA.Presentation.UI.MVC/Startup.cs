using Microsoft.Owin;
using Owin;
using DAFA.Presentation.UI.MVC;

[assembly: OwinStartup(typeof(Startup))]
namespace DAFA.Presentation.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}