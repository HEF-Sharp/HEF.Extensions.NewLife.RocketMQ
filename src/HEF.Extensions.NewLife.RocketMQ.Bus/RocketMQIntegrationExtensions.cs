using HEF.MQ.Bus;
using System;

namespace NewLife.RocketMQ.Bus
{
    public static class RocketMQIntegrationExtensions
    {
        public static void UsingRocketMQ(this IMQBusRegisterConfigurator configurator,
            Action<IMQBusRegistration, IRocketMQFactoryConfigurator> configure = null)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var factory = new RocketMQBusFactory(configure);
            configurator.SetBusFactory(factory);
        }
    }
}
