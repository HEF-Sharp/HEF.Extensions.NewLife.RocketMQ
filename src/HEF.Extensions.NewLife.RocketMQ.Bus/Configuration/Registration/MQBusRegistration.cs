using System;
using System.Collections.Generic;

namespace NewLife.RocketMQ.Bus
{
    public interface IMQBusRegistration
    {
        void ConfigureMessageConsumer<TMessageConsumer>(IMQConsumerConfigurator configurator)
            where TMessageConsumer : class, IMessageConsumer;
    }

    public class MQBusRegistration : IMQBusRegistration
    {
        private readonly IMQServiceProvider _provider;
        private readonly IReadOnlyDictionary<Type, IMQMessageConsumerRegistration> _messageConsumers;        

        public MQBusRegistration(IMQServiceProvider provider, IReadOnlyDictionary<Type, IMQMessageConsumerRegistration> messageConsumers)
        {
            _provider = provider;
            _messageConsumers = messageConsumers;
        }

        public void ConfigureMessageConsumer<TMessageConsumer>(IMQConsumerConfigurator configurator)
            where TMessageConsumer : class, IMessageConsumer
        {
            var messageConsumerType = typeof(TMessageConsumer);

            if (!_messageConsumers.TryGetValue(messageConsumerType, out var messageConsumer))
                throw new ArgumentException($"The message consumer type was not found: {messageConsumerType.Name}", nameof(TMessageConsumer));

            messageConsumer.AttachToConsumer(configurator, _provider);
        }
    }
}
