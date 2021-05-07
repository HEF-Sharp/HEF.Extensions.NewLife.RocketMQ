using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace HEF.MQ.Bus
{
    public class DependencyInjectionMQServiceContainer : IMQServiceContainer
    {
        private readonly IServiceCollection _collection;

        public DependencyInjectionMQServiceContainer(IServiceCollection collection)
        {
            _collection = collection;
        }        

        void IMQServiceContainer.RegisterMessageConsumer<TMessageConsumer>()
        {
            _collection.AddScoped<TMessageConsumer>();
        }

        void IMQServiceContainer.RegisterScoped<TService, TImplementation>()
        {
            _collection.TryAddScoped<TService, TImplementation>();
        }

        void IMQServiceContainer.RegisterSingleton<TService, TImplementation>()
        {
            _collection.TryAddSingleton<TService, TImplementation>();
        }

        public void RegisterScoped<TService>(Func<IMQServiceProvider, TService> factoryMethod) where TService : class
        {
            _collection.TryAddScoped(provider => factoryMethod.Invoke(new DependencyInjectionMQServiceProvider(provider)));
        }

        public void RegisterSingleton<TService>(Func<IMQServiceProvider, TService> factoryMethod) where TService : class
        {
            _collection.TryAddSingleton(provider => factoryMethod.Invoke(new DependencyInjectionMQServiceProvider(provider)));
        }
    }
}
