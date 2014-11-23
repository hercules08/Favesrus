using Favit.BLL.Interfaces;
using Favit.BLL.Services;
using Favit.DAL;
using Favit.Model.Entities;
using Favit.Server;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
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

            //container.Bind<IRepository>().To<Rep
        }

        private void ConfigureEntityFramework(IKernel container)
        {
            container.Bind<IDataContextAsync>().To<FavitDBContext>();
            container.Bind<IRepositoryProvider>().ToProvider(new RepositoryFactoryProvider()).InSingletonScope();
            container.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InSingletonScope();

            container.Bind<IRepositoryAsync<Retailer>>().To<Repository<Retailer>>();
            container.Bind<IRetailerService>().To<RetailerService>();

            //container.Bind<IUnitOfWork>().To<UnitOfWork>();
            //var sessionFactory = object;
            //container.Bind<ISessionFactory>().ToConstant(sessionFactory);
            //container.Bind<IUnitOfWork>().To
        }

        public class RepositoryFactoryProvider : Provider<IRepositoryProvider>
        {
            protected override IRepositoryProvider CreateInstance(IContext context)
            {
                return new RepositoryProvider(new RepositoryFactories());
            }
        }

    }
}