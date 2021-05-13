using System;
using System.Collections.Generic;

namespace HEF.MQ.Bus
{
    public interface IMQBusRegistration
    {
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
        private readonly IMQServiceProvider _provider;
        private readonly IReadOnlyDictionary<Type, IMQMessageConsumerRegistration> _messageConsumerDict;        

        public MQBusRegistration(IMQServiceProvider provider, IReadOnlyDictionary<Type, IMQMessageConsumerRegistration> messageConsumerDict)
        {
            _provider = provider;
            _messageConsumerDict = messageConsumerDict;
        }

        public void ConfigureMessageConsumer<TMessage, TMessageConsumer>(IMQConsumerConfigurator<TMessage> configurator)
            where TMessage : class
            where TMessageConsumer : class, IMQMessageConsumer
        {
            var messageConsumerRegistration = GetMessageConsumerRegistration<TMessageConsumer>();

            messageConsumerRegistration.AttachToConsumer(configurator, _provider);
        }

        public void ConfigureTypedMessageConsumer<TMessage, TContent, TMessageConsumer>(IMQConsumerConfigurator<TMessage> configurator)
            where TMessage : class
            where TContent : class
            where TMessageConsumer : class, IMQMessageConsumer
        {
            var messageConsumerRegistration = GetMessageConsumerRegistration<TMessageConsumer>();

            messageConsumerRegistration.AttachToConsumer<TMessage, TContent>(configurator, _provider);
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
