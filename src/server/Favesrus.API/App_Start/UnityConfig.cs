using Favesrus.ApiService;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using Favesrus.DAL;
using Favesrus.DAL.Core;
using Favesrus.Domain.Entity;
using Favesrus.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace Favesrus.API
{
    
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<Favesrus.API.Controllers.AccountController>();

            container.RegisterType<ILogManager, LogManager>();
            container.RegisterType<IAutoMapper, AutoMapperAdapter>();

            ConfigureDAL(container);
            ConfigureIdentityOwin(container);
            
            // Services
            ConfigureServices(container);
        }

        private static void ConfigureServices(IUnityContainer container)
        {
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<ICommunicationService, CommunicationService>();
            container.RegisterType<IGiftItemService, GiftItemService>();
            container.RegisterType<IFaveEventService, FaveEventService>();
            container.RegisterType<ICategoryService, CategoryService>();
        }

        private static void ConfigureIdentityOwin(IUnityContainer container)
        {
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<FavesrusUserManager>();
           
            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            
            container.RegisterType<IUserStore<FavesrusUser>, UserStore<FavesrusUser>>(
              new InjectionConstructor(typeof(FavesrusDbContext)));
        }

        private static void ConfigureDAL(IUnityContainer container)
        {
            container.RegisterType(typeof(IRepository<>), typeof(RepositoryBase<>));
            container.RegisterType<IDatabaseFactory, DatabaseFactory>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<FavesrusDbContext>();
        }
    }
}
