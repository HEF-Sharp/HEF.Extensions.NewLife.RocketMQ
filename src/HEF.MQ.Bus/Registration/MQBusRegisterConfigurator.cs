using System;
using System.Collections.Concurrent;

namespace HEF.MQ.Bus
{
    public interface IMQBusRegisterConfigurator
    {
        IMQServiceContainer Container { get; }

        void AddMessageConsumer<TMessageConsumer>() where TMessageConsumer : class, IMessageConsumer;
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
            where TMessageConsumer : class, IMessageConsumer
        {
            _messageConsumers.GetOrAdd(typeof(TMessageConsumer), (type) =>
            {
                Container.RegisterMessageConsumer<TMessageConsumer>();

                return new MQMessageConsumerRegistration<TMessageConsumer>();
            });
        }

        protected IMQBusRegistration CreateRegistration(IMQServiceProvider provider)
        {
            return new MQBusRegistration(provider, _messageConsumers);
        }
    }
}
