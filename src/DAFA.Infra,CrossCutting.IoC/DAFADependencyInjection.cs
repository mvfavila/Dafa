using DAFA.Application.AppServices;
using DAFA.Application.Interfaces;
using DAFA.Domain.Interfaces.Repository;
using DAFA.Domain.Interfaces.Repository.ReadOnly;
using DAFA.Domain.Interfaces.Services;
using DAFA.Domain.Services;
using DAFA.Infra.CrossCutting.Identity;
using DAFA.Infra.CrossCutting.Identity.Configuration;
using DAFA.Infra.CrossCutting.Identity.Context;
using DAFA.Infra.Data.Context;
using DAFA.Infra.Data.Repositories;
using DAFA.Infra.Data.Repositories.ReadOnly;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace DAFA.Infra.CrossCutting.IoC
{
    public class DAFADependencyInjection
    {
        public static void RegisterServices(Container container)
        {
            // Infra

            // Infra - Data

            container.Register<DAFAContext>(Lifestyle.Scoped);
            container.Register<AppDbContext>(Lifestyle.Scoped);

            container.Register<IEventRepository, EventRepository>(Lifestyle.Scoped);
            container.Register<IEventReadOnlyRepository, EventReadOnlyRepository>(Lifestyle.Scoped);
            container.Register<IEventWarningRepository, EventWarningRepository>(Lifestyle.Scoped);
            container.Register<IEventWarningReadOnlyRepository, EventWarningReadOnlyRepository>(Lifestyle.Scoped);
            container.Register<IEventTypeRepository, EventTypeRepository>(Lifestyle.Scoped);
            container.Register<IEventTypeReadOnlyRepository, EventTypeReadOnlyRepository>(Lifestyle.Scoped);
            container.Register<IPeriodicityRepository, PeriodicityRepository>(Lifestyle.Scoped);
            container.Register<IPeriodicityReadOnlyRepository, PeriodicityReadOnlyRepository>(Lifestyle.Scoped);
            container.Register<IClientRepository, ClientRepository>(Lifestyle.Scoped);
            container.Register<IClientReadOnlyRepository, ClientReadOnlyRepository>(Lifestyle.Scoped);
            container.Register<IFieldRepository, FieldRepository>(Lifestyle.Scoped);
            container.Register<IFieldReadOnlyRepository, FieldReadOnlyRepository>(Lifestyle.Scoped);
            container.Register<IMenuReadOnlyRepository, MenuReadOnlyRepository>(Lifestyle.Scoped);

            // Infra - CrossCutting - Identity

            container.Register<IUserStore<ApplicationUser>>(
                () => new UserStore<ApplicationUser>(
                    new AppDbContext()
                ),
                Lifestyle.Scoped
            );

            container.Register<IRoleStore<IdentityRole, string>>(
                () => new RoleStore<IdentityRole>(),
                Lifestyle.Scoped
            );

            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            // Domain

            container.Register<IEventService, EventService>(Lifestyle.Scoped);
            container.Register<IEventWarningService, EventWarningService>(Lifestyle.Scoped);
            container.Register<IEventTypeService, EventTypeService>(Lifestyle.Scoped);
            container.Register<IPeriodicityService, PeriodicityService>(Lifestyle.Scoped);
            container.Register<IClientService, ClientService>(Lifestyle.Scoped);
            container.Register<IFieldService, FieldService>(Lifestyle.Scoped);
            container.Register<IMenuService, MenuService>(Lifestyle.Scoped);

            // Application

            container.Register<IEventAppService, EventAppService>(Lifestyle.Scoped);
            container.Register<IEventWarningAppService, EventWarningAppService>(Lifestyle.Scoped);
            container.Register<IEventTypeAppService, EventTypeAppService>(Lifestyle.Scoped);
            container.Register<IPeriodicityAppService, PeriodicityAppService>(Lifestyle.Scoped);
            container.Register<IClientAppService, ClientAppService>(Lifestyle.Scoped);
            container.Register<IFieldAppService, FieldAppService>(Lifestyle.Scoped);
            container.Register<IMenuAppService, MenuAppService>(Lifestyle.Scoped);
            container.Register<IWarningsProcessingAppService, WarningsProcessingAppService>(Lifestyle.Scoped);
        }
    }
}
