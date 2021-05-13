using System;
using System.Collections.Concurrent;

namespace HEF.MQ.Bus
{
    public interface IMQBusRegisterConfigurator
    {
        IMQServiceContainer Container { get; }

        void AddMessageConsumer<TMessageConsumer>() where TMessageConsumer : class, IMQMessageConsumer;

        void SetBusFactory<TBusFactory>(TBusFactory busFactory) where TBusFactory : IMQBusFactory;
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

        public void SetBusFactory<TBusFactory>(TBusFactory busFactory)
            where TBusFactory : IMQBusFactory
        {
            if (busFactory == null)
                throw new ArgumentNullException(nameof(busFactory));

            Container.RegisterSingleton(provider => CreateRegistration(provider));
            Container.RegisterSingleton(provider => busFactory.CreateMQBus(provider.GetRequiredService<IMQBusRegistration>()));
        }

        protected IMQBusRegistration CreateRegistration(IMQServiceProvider provider)
        {
            return new MQBusRegistration(provider, _messageConsumers);
        }
    }
}
