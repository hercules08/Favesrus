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

namespace Favesrus.Server
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
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
        }
    }
}