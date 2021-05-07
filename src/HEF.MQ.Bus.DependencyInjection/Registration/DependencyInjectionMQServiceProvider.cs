using System;
using Microsoft.Extensions.DependencyInjection;

namespace HEF.MQ.Bus
{
    public class DependencyInjectionMQServiceProvider : IMQServiceProvider
    {
        private readonly IServiceProvider _provider;

        public DependencyInjectionMQServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IMQServiceScope CreateScope()
        {
            return new DependencyInjectionMQServiceScope(_provider.CreateScope());
        }

        public T GetService<T>() where T : class
        {
            return _provider.GetService<T>();
        }

        public T GetRequiredService<T>() where T : class
        {
            return _provider.GetRequiredService<T>();
        }
    }

    public class DependencyInjectionMQServiceScope : IMQServiceScope
    {
        private readonly IServiceScope _serviceScope;
        private Lazy<IMQServiceProvider> _serviceProvider;

        public DependencyInjectionMQServiceScope(IServiceScope serviceScope)
        {
            _serviceScope = serviceScope;

            _serviceProvider = new Lazy<IMQServiceProvider>(() => new DependencyInjectionMQServiceProvider(_serviceScope.ServiceProvider));
        }

        public IMQServiceProvider ServiceProvider => _serviceProvider.Value;

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
