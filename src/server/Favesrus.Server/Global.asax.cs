using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Web.Optimization;
using Favesrus.Server.Infrastructure.Impl;
using Favesrus.Server.Infrastructure;
using Favesrus.Server.Infrastructure.Interface;
using Newtonsoft.Json.Serialization;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace Favesrus.Server
{
    public class Global : HttpApplication
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        void Application_Start(object sender, EventArgs e)
        {
            if (log.IsInfoEnabled) log.Info("Starting Favesrus Website");

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Configure Dependency Resolver for MVC
            System.Web.Mvc.DependencyResolver.SetResolver(
                (System.Web.Mvc.IDependencyResolver)
                GlobalConfiguration.Configuration.DependencyResolver);


            // Configure AutoMapper from Domain to Dto
            AutoMapperConfigurator.Configure(WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());

            //Prevent Formatting Loops
            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Use camel casing
            GlobalConfiguration.Configuration.Formatters.JsonFormatter
                .SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}