using Favit.BLL;
using Favit.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Favit.Server
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        //http://blog.longle.net/2013/07/30/bounded-dbcontext-with-generic-unit-of-work-generic-repositories-entity-framework-6-unity-3-0-in-mvc-4/
        //http://www.davepaquette.com/archive/2013/03/27/managing-entity-framework-dbcontext-lifetime-in-asp-net-mvc.aspx
        //http://blog.longle.net/2013/05/17/generically-implementing-the-unit-of-work-repository-pattern-with-entity-framework-in-mvc-simplifying-entity-graphs-part-2-mvc-4-di-ioc-with-mef-attributeless-conventions/
        //https://genericunitofworkandrepositories.codeplex.com/
        //http://blog.longle.net/2013/05/11/genericizing-the-unit-of-work-pattern-repository-pattern-with-entity-framework-in-mvc/

        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (!DebuggingService.RunningInDebugMode())
            {
                Database.SetInitializer<FavitDBContext>(new FavitInitializer());
            }
            else
            {
                Database.SetInitializer<FavitDBContext>(new FavitInitializer());
            }
        }
    }
}
