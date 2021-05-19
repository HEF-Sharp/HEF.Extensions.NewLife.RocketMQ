using System;
using System.Collections.Generic;

namespace HEF.MQ.Bus
{
    public interface IMQBusRegistration
    {
        IMQServiceProvider ServiceProvider { get; }

        void ConfigureMessageConsumer<TMessage, TMessageConsumer>(IMQConsumerConfigurator<TMessage> configurator)
            where TMessage : class
            where TMessageConsumer : class, IMQMessageConsumer;

        void ConfigureTypedMessageConsumer<TMessage, TContent, TMessageConsumer>(IMQConsumerConfigurator<TMessage> configurator)
            where TMessage : class
            where TContent : class
            where TMessageConsumer : class, IMQMessageConsumer;
    }

    public class MQBusRegistration : IMQBusRegistration
    {
        private readonly IReadOnlyDictionary<Type, IMQMessageConsumerRegistration> _messageConsumerDict;        

        public MQBusRegistration(IMQServiceProvider provider, IReadOnlyDictionary<Type, IMQMessageConsumerRegistration> messageConsumerDict)
        {
            ServiceProvider = provider;
            _messageConsumerDict = messageConsumerDict;
        }

        public IMQServiceProvider ServiceProvider { get; }

        public void ConfigureMessageConsumer<TMessage, TMessageConsumer>(IMQConsumerConfigurator<TMessage> configurator)
            where TMessage : class
            where TMessageConsumer : class, IMQMessageConsumer
        {
            var messageConsumerRegistration = GetMessageConsumerRegistration<TMessageConsumer>();

            messageConsumerRegistration.AttachToConsumer(configurator, ServiceProvider);
        }

        public void ConfigureTypedMessageConsumer<TMessage, TContent, TMessageConsumer>(IMQConsumerConfigurator<TMessage> configurator)
            where TMessage : class
            where TContent : class
            where TMessageConsumer : class, IMQMessageConsumer
        {
            var messageConsumerRegistration = GetMessageConsumerRegistration<TMessageConsumer>();

            messageConsumerRegistration.AttachToConsumer<TMessage, TContent>(configurator, ServiceProvider);
        }

        #region Helper Functions
        private IMQMessageConsumerRegistration GetMessageConsumerRegistration<TMessageConsumer>()
        {
            var messageConsumerType = typeof(TMessageConsumer);

            if (!_messageConsumerDict.TryGetValue(messageConsumerType, out var messageConsumerRegistration))
                throw new ArgumentException($"The message consumer type was not found: {messageConsumerType.Name}", nameof(TMessageConsumer));

            return messageConsumerRegistration;
        }
        #endregion
    }
}
