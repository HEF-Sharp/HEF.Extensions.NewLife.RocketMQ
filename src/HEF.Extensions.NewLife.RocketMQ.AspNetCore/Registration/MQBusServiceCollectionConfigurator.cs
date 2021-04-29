using Microsoft.Extensions.DependencyInjection;
using NewLife.RocketMQ.Bus;

namespace NewLife.RocketMQ.AspNetCore
{
    public class MQBusServiceCollectionConfigurator : MQBusRegisterConfigurator
    {
        public MQBusServiceCollectionConfigurator(IServiceCollection collection)
            : base(new MQServiceCollectionContainer(collection))
        { }
    }
}
