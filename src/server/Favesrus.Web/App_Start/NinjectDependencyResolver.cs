using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace Favesrus.Server
{
    public class NinjectDependencyResolver:IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel _container;

        public IKernel Container
        {
            get { return _container; }
        }

        public NinjectDependencyResolver() : this(new StandardKernel())
        {

        }

        public NinjectDependencyResolver(IKernel container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            // noop
        }
    }
}