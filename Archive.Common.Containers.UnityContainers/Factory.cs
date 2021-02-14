namespace Archive.Common.Containers.UnityContainers
{
    public abstract class Factory
    {
        private readonly UnityDependenciesContainer _container;

        public Factory(params RegInfo[] registrations)
        {
            _container = new UnityDependenciesContainer();

            foreach (var info in registrations)
            {
                _container.RegisterInstance(info.Type, info.Implementation);
            }
        }

        protected IDependenciesContainer GetContainer()
        {
            return _container;
        }
    }
}
