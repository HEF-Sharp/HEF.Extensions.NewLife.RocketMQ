using Microsoft.Extensions.DependencyInjection;

namespace HEF.MQ.Bus
{
    public class ServiceCollectionMQBusConfigurator : MQBusRegisterConfigurator
    {
        public ServiceCollectionMQBusConfigurator(IServiceCollection collection)
            : base(new DependencyInjectionMQServiceContainer(collection))
        { }
    }
}
