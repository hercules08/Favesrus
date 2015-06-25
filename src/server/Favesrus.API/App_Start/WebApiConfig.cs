using Favesrus.API.Results.Error;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace Favesrus.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.DependencyResolver = new NinjectDependencyResolver();

            // Remove xml formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Enable CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            
            // Web API configuration and services
            ConfigureRouting(config);

            //TODO Enable tracewriting and logging

            //TODO Enable exception logger

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            //Prevent Formatting Loops
            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Use camel casing
            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
        }

        private static void ConfigureRouting(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
