using System;
using Unity;
using Unity.Lifetime;

namespace Archive.Common.Containers.UnityContainers
{
    public class UnityDependenciesContainer : IDependenciesContainer
    {
        private readonly IUnityContainer _container;

        public UnityDependenciesContainer()
        {
            _container = new UnityContainer();
        }

        private UnityDependenciesContainer(IUnityContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        public void RegisterInstance(Type registrationType, object instance)
        {            
            _container.RegisterInstance(registrationType, instance);
        }

        public void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _container.RegisterType<TInterface, TImplementation>();
        }

        public void RegisterExternallyControlledSingletone<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _container.RegisterType(typeof(TInterface), typeof(TImplementation), lifetimeManager: new ExternallyControlledLifetimeManager());
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public IDependenciesContainer CreateChildContainer()
        {
            return new UnityDependenciesContainer(_container.CreateChildContainer());
        }

        #region Dispose

        ~UnityDependenciesContainer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _container.Dispose();
            }
        }

        #endregion
    }
}
