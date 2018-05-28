using DAFA.Services.WebAPI.App_Start;
using WebActivator;

[assembly: PostApplicationStartMethod(typeof(SimpleInjectorWebApiInitializer), "Initialize")]

namespace DAFA.Services.WebAPI.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using System.Web;
    using System.Reflection;
    using DAFA.Infra.CrossCutting.IoC;
    using SimpleInjector.Lifestyles;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>Initialize the container and register it as Web API Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            InitializeContainer(container);

            //// Needed to register the Owin ambient which is an Identity dependency
            //// Executed outside of the IoC layer so System.Web does not get referenced.
            //container.Register(() =>
            //{
            //    if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null
            //    && container.IsVerifying())
            //    {
            //        return new OwinContext().Authentication;
            //    }
            //    return HttpContext.Current.GetOwinContext().Authentication;

            //},
            //Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            //container.RegisterWebApiControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            DAFADependencyInjection.RegisterServices(container);
        }
    }
}