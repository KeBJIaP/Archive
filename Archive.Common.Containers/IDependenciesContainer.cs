using System;

namespace Archive.Common.Containers
{
    public interface IDependenciesContainer : IDisposable
    {
        T Resolve<T>();

        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void RegisterExternallyControlledSingletone<TInterface, TImplementation>() where TImplementation : TInterface;

        void RegisterInstance(Type registrationType, object instance);
        void RegisterInstance<T>(T instance);

        IDependenciesContainer CreateChildContainer();
    }
}
