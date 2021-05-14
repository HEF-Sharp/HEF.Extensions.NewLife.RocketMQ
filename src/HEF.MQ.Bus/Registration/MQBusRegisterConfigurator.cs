using System;
using System.Collections.Concurrent;

namespace HEF.MQ.Bus
{
    public interface IMQBusRegisterConfigurator
    {
        IMQServiceContainer Container { get; }

        void AddMessageConsumer<TMessageConsumer>() where TMessageConsumer : class, IMQMessageConsumer;

        void SetBusFactory<TProducerProvider>(IMQBusFactory<TProducerProvider> busFactory) where TProducerProvider : class;
    }

    public class MQBusRegisterConfigurator : IMQBusRegisterConfigurator
    {
        private readonly ConcurrentDictionary<Type, IMQMessageConsumerRegistration> _messageConsumers = new();

        public MQBusRegisterConfigurator(IMQServiceContainer container)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IMQServiceContainer Container { get; }

        public void AddMessageConsumer<TMessageConsumer>()
            where TMessageConsumer : class, IMQMessageConsumer
        {
            _messageConsumers.GetOrAdd(typeof(TMessageConsumer), (type) =>
            {
                Container.RegisterMessageConsumer<TMessageConsumer>();

                return new MQMessageConsumerRegistration<TMessageConsumer>();
            });
        }

        public void SetBusFactory<TProducerProvider>(IMQBusFactory<TProducerProvider> busFactory)
            where TProducerProvider : class
        {
            if (busFactory == null)
                throw new ArgumentNullException(nameof(busFactory));

            Container.RegisterSingleton(provider => CreateRegistration(provider));
            Container.RegisterSingleton(provider => busFactory.CreateMQBus(provider.GetRequiredService<IMQBusRegistration>()));

            Container.RegisterSingleton(provider => provider.GetRequiredService<IMQBus<TProducerProvider>>() as IMQBusControl);
            Container.RegisterSingleton(provider => provider.GetRequiredService<IMQBus<TProducerProvider>>().ProducerProvider);
        }

        protected IMQBusRegistration CreateRegistration(IMQServiceProvider provider)
        {
            return new MQBusRegistration(provider, _messageConsumers);
        }
    }
}
