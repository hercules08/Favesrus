using Favit.Server.Interfaces;
using Favit.Server.Models;
using Favit.Server.Web.Common;
using Ninject;
using Ninject.Web.Common;
using System.Web.Http;
//using Ninject.Mv

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

            //container.Bind<IRepository>().To<Rep
        }

        private void ConfigureEntityFramework(IKernel container)
        {
            FavitDBContext context = new FavitDBContext();

            container.Bind<FavitDBContext>().ToConstant(context).InRequestScope();
            container.Bind<IUnitOfWork>().To<UnitOfWork>();
            var sessionFactory = object;
            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            container.Bind<IUnitOfWork>().To
        }

    }
}