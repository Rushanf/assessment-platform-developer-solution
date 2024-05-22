using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Queries;
using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using assessment_platform_developer.Infrastructure.Repositories;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;
using SimpleInjector;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.UI;
using assessment_platform_developer.Services.Interfaces;
using assessment_platform_developer.Services;

namespace assessment_platform_developer.Configs
{
    public static class DependencyInjectionConfig
    {
		private static Container container;

        public static Container Initialize()
        {
            // 1. Create a new Simple Injector container.
            container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // 2. Configure the container (register)
            RegisterServices(container);

            // Register your Page classes to allow them to be verified and diagnosed.
            RegisterWebPages(container);

            container.Options.ResolveUnregisteredConcreteTypes = true;

            // 3. Verify the container's configuration.
            container.Verify();

            return container;
        }

        private static void RegisterServices(Container container)
        {
            // Register Commands
            container.Register<ICommandHandler<CreateCustomerCommand>, CreateCustomerCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<UpdateCustomerCommand>, UpdateCustomerCommandHandler>(Lifestyle.Scoped);
            container.Register<ICommandHandler<DeleteCustomerCommand>, DeleteCustomerCommandHandler>(Lifestyle.Scoped);

            // Register Queries
            container.Register<IQueryHandler<GetAllCustomersQuery, List<CustomerBasicResponse>>, GetAllCustomersQueryHandler>(Lifestyle.Scoped);
            container.Register<IQueryHandler<GetCustomerQuery, CustomerResponse>, GetCustomerQueryHandler>(Lifestyle.Scoped);

            // Register Repositories
            container.Register<IReadRepository<Customer>, ReadCustomerRepository>(Lifestyle.Scoped);
            container.Register<IWriteRepository<Customer>, WriteCustomerRepository>(Lifestyle.Scoped);
            //container.Register<IReadRepository<Customer>, ReadCustomerDBRepository>(Lifestyle.Scoped);
            //container.Register<IWriteRepository<Customer>, WriteCustomerDBRepository>(Lifestyle.Scoped);

            // Register Services
            container.Register<ICustomerService, CustomerService>(Lifestyle.Scoped);
        }

        private static void RegisterWebPages(Container container)
        {
            var pageTypes =
                from assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                where !assembly.IsDynamic
                where !assembly.GlobalAssemblyCache
                from type in assembly.GetExportedTypes()
                where type.IsSubclassOf(typeof(Page))
                where !type.IsAbstract && !type.IsGenericType
                select type;

            foreach (var type in pageTypes)
            {
                var reg = Lifestyle.Transient.CreateRegistration(type, container);
                reg.SuppressDiagnosticWarning(
                    DiagnosticType.DisposableTransientComponent,
                    "ASP.NET creates and disposes page classes for us.");
                container.AddRegistration(type, reg);
            }
        }
    }
}