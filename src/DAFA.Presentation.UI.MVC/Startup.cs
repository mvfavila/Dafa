using Microsoft.Owin;
using Owin;
using DAFA.Presentation.UI.MVC;
using DAFA.Infra.CrossCutting.Logging;

[assembly: OwinStartup(typeof(Startup))]
namespace DAFA.Presentation.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Info("Aplicação sendo iniciada.");
            ConfigureAuth(app);
        }
    }
}