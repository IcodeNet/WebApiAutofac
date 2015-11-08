using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using NLog;
using WebApiAutofac.Controllers;
using WebApiAutofac.Filters;

namespace WebApiAutofac
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );


            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLogger>().As<ILogger>();

            builder.Register(c => new LoggingActionFilter(c.Resolve<ILogger>())).
                AsWebApiActionFilterFor<ValuesController>(c => c.Get())
                .InstancePerRequest();

            var globalConfiguration = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);


        }
    }
}