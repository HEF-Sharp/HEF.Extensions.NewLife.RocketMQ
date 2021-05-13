using Microsoft.Extensions.DependencyInjection;

namespace HEF.MQ.Bus
{
    public interface IServiceCollectionMQBusConfigurator : IMQBusRegisterConfigurator
    {
        IServiceCollection Collection { get; }
    }

    public class ServiceCollectionMQBusConfigurator : MQBusRegisterConfigurator, IServiceCollectionMQBusConfigurator
    {
        public ServiceCollectionMQBusConfigurator(IServiceCollection collection)
            : base(new DependencyInjectionMQServiceContainer(collection))
        {
            Collection = collection;
        }

        public IServiceCollection Collection { get; }
    }
}
