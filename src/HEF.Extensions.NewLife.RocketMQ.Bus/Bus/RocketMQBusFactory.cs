using HEF.MQ.Bus;
using System;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQBusFactory : IMQBusFactory<IRocketMQProducerProvider>
    {
        private readonly Action<IMQBusRegistration, IRocketMQFactoryConfigurator> _configure;

        public RocketMQBusFactory(Action<IMQBusRegistration, IRocketMQFactoryConfigurator> configure)
        {
            _configure = configure;
        }

        public IMQBus<IRocketMQProducerProvider> CreateMQBus(IMQBusRegistration registration)
        {
            var configurator = new RocketMQFactoryConfigurator();

            _configure?.Invoke(registration, configurator);

            return configurator.Build(registration);
        }
    }
}
