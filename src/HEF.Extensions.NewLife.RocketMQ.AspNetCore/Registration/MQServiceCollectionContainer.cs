using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewLife.RocketMQ.Bus;
using System;

namespace NewLife.RocketMQ.AspNetCore
{
    public class MQServiceCollectionContainer : IMQServiceContainer
    {
        private readonly IServiceCollection _collection;

        public MQServiceCollectionContainer(IServiceCollection collection)
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

        public void RegisterScoped<TService>(Func<IServiceProvider, TService> factoryMethod) where TService : class
        {
            _collection.TryAddScoped((provider) => factoryMethod.Invoke(provider));
        }

        public void RegisterSingleton<TService>(Func<IServiceProvider, TService> factoryMethod) where TService : class
        {
            _collection.TryAddSingleton((provider) => factoryMethod.Invoke(provider));
        }
    }
}
