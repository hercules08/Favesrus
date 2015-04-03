using Favesrus.Server.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace Favesrus.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Remove xml formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //TODO Enable CORS
            //config.EnableCors();
            
            // Web API configuration and services
            ConfigureRouting(config);

            //TODO Enable tracewriting and logging

            //TODO Enable exception logger

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            
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
