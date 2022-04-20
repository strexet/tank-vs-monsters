namespace Infrastructure.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator instance;

        public static ServiceLocator Container => instance ??= new ServiceLocator();

        private ServiceLocator() { }

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            Implementation<TService>.serviceInstance = implementation;

        public TService Single<TService>() where TService : IService => Implementation<TService>.serviceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService serviceInstance;
        }
    }
}