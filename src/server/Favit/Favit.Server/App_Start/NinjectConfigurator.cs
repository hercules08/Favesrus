﻿using Favit.BLL;
using Favit.BLL.Interfaces;
using Favit.BLL.Services;
using Favit.DAL;
using Favit.DAL.EntityFramwork;
using Favit.DAL.Interfaces;
using Favit.Model.Entities;
using Favit.Server;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using System.Data.Common;
using System.Data.Entity;
using System.Web.Http;

namespace Favit.Server.App_Start
{
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);

            var resolver = new NinjectDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        private void AddBindings(IKernel container)
        {
            ConfigureEntityFramework(container);

            container.Bind<IItemService>().To<ItemService>();
            container.Bind<IRetailerService>().To<RetailerService>();
        }

        private void ConfigureEntityFramework(IKernel container)
        {
            container.Bind<FavitDBContext>().ToProvider(new FavitContextProvider());
            container.Bind<IUnitOfWork>().To<UnitOfWork>();
            container.Bind<IRepository>().To<Repository>();
            container.Bind<ISessionFactory>().To<SessionFactory>().InRequestScope();
        }

        public class FavitContextProvider : Provider<FavitDBContext>
        {
            protected override FavitDBContext CreateInstance(IContext context)
            {
                if (!DebuggingService.RunningInDebugMode())
                {
                    return new FavitDBContext("name=DeployedContext");
                }
                else
                {
                    return new FavitDBContext();
                }
            }
        }

    }
}