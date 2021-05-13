using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HEF.MQ.Bus
{
    public static class DependencyInjectionRegistrationExtensions
    {
        public static IServiceCollection AddMQBus(this IServiceCollection collection, Action<IServiceCollectionMQBusConfigurator> configure = null)
        {
            if (collection.Any(d => d.ServiceType == typeof(IMQBus)))
            {
                throw new InvalidOperationException("AddMQBus() was already called and may only be called once per container.");
            }

            var configurator = new ServiceCollectionMQBusConfigurator(collection);

            configure?.Invoke(configurator);

            return collection;
        }
    }
}
