using SimpleInjector;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using assessment_platform_developer.Configs;

namespace assessment_platform_developer
{
	public sealed class PageInitializerModule : IHttpModule
	{
		public static void Initialize()
		{
			DynamicModuleUtility.RegisterModule(typeof(PageInitializerModule));
		}

		void IHttpModule.Init(HttpApplication app)
		{
			app.PreRequestHandlerExecute += (sender, e) =>
			{
				var handler = app.Context.CurrentHandler;
				if (handler != null)
				{
					string name = handler.GetType().Assembly.FullName;
					if (!name.StartsWith("System.Web") &&
						!name.StartsWith("Microsoft"))
					{
						Global.InitializeHandler(handler);
					}
				}
			};
		}

		void IHttpModule.Dispose() { }
	}

	public class Global : HttpApplication
	{
		private static Container container;

		public static void InitializeHandler(IHttpHandler handler)
		{
			var handlerType = handler is Page
				? handler.GetType().BaseType
				: handler.GetType();
			container.GetRegistration(handlerType, true).Registration
				.InitializeInstance(handler);
		}

		void Application_Start(object sender, EventArgs e)
		{
			// Code that runs on application startup
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			// Bootstrap the dependency injection
            container = DependencyInjectionConfig.Initialize();

            HttpContext.Current.Application["DIContainer"] = container;
		}
	}
}