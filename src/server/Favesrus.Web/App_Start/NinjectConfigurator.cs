
//namespace Favesrus.Server
//{
//    public class NinjectConfigurator
//    {
//        /// <summary>
//        /// Entry method used by caller to configure the given 
//        /// container with all of this application's 
//        /// dependencies. Also configures the container as this
//        /// application's dependency resolver.
//        /// </summary>
//        public void Configure(IKernel container)
//        {
//            // Add all bindings/dependencies
//            AddBindings(container);

//            // Use the container and our NinjectDependencyResolver as
//            // application's resolver
//            //var resolver = new NinjectDependencyResolver(container);
//            //GlobalConfiguration.Configuration.DependencyResolver = resolver;
//        }

//        /// <summary>
//        /// Add all bindings/dependencies to the container
//        /// </summary>
//        private void AddBindings(IKernel container)
//        {
//            ConfigureDAL(container);

//            // Services
//            ConfigureServices(container);

//            // AutoMapper Config
//            ConfigureAutoMapper(container);

//            // Processor Config
//            ConfigureProcessors(container);

//        }

//        private void ConfigureDAL(IKernel container)
//        {
//            //container.Bind<IFavesrusDbContext>().To<FavesrusDbContext>().InRequestScope();
//            //container.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
//            //container.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
//        }

//        private void ConfigureServices(IKernel container)
//        {
//            //container.Bind(typeof(IRetailerService)).To(typeof(RetailerService));
//            //container.Bind(typeof(IGiftItemService)).To(typeof(GiftItemService));
//            //container.Bind(typeof(ICategoryService)).To(typeof(CategoryService));
//            //container.Bind(typeof(IRecommendationService)).To(typeof(RecommendationService));
//            ////container.Bind<IShopService>().To<ShopService>();
//            //container.Bind<IEmailService>().To<EmailService>();

//            // BLL
//            //container.Bind<IShopFinder>().To<ShopFinder>();
//        }

//        private void ConfigureAutoMapper(IKernel container)
//        {
//            //container.Bind<IAutoMapper>()
//            //    .To<AutoMapperAdapter>()
//            //    .InSingletonScope();

//            //container.Bind<IAutoMapperTypeConfigurator>()
//            //    .To<FavesrusUser_DtoFavesrusUser>()
//            //    .InSingletonScope();

//            //container.Bind<IAutoMapperTypeConfigurator>()
//            //    .To<DtoFavesrusUser_FavesrusUser>()
//            //    .InSingletonScope();

//            //container.Bind<IAutoMapperTypeConfigurator>()
//            //    .To<RegisterModel_FavesrusUser>()
//            //    .InSingletonScope();

//            //container.Bind<IAutoMapperTypeConfigurator>()
//            //    .To<FavesrusUser_RegisterModel>()
//            //    .InSingletonScope();

//            //container.Bind<IAutoMapperTypeConfigurator>()
//            //    .To<DtoGiftItem_GiftItem>()
//            //    .InSingletonScope();

//            //container.Bind<IAutoMapperTypeConfigurator>()
//            //    .To<GiftItem_DtoGiftItem>()
//            //    .InSingletonScope();
//        }

//        private void ConfigureProcessors(IKernel container)
//        {
//            //container.Bind<IAccountProcessor>()
//            //    .To<AcccountProcessor>();

//            //container.Bind<IRecommendationsProcessor>()
//            //    .To<RecommendationsProcessor>();
//        }
//    }
//}