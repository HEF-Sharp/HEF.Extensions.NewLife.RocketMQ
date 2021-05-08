using System;

namespace HEF.MQ.Bus
{
    public class MQConsumerContextFactory<TMessage, TMessageConsumer>
        where TMessage : class
        where TMessageConsumer : class, IMQMessageConsumer
    {
        private readonly IMQServiceProvider _provider;

        public MQConsumerContextFactory(IMQServiceProvider provider)
        {
            _provider = provider;
        }

        public MQConsumerContext<TMessage, IMQMessageConsumer<TMessage>> CreateConsumerContext<TContent>(TMessage message)
            where TContent : class
        {
            var messageConsumer = _provider.GetRequiredService<TMessageConsumer>();
            if (messageConsumer == null)
                throw new NotImplementedException($"Unable to resolve message consumer type '{typeof(TMessageConsumer).Name}'");

            if (messageConsumer is IMQMessageConsumer<TMessage> targetMessageConsumer)
                return new MQConsumerContext<TMessage, IMQMessageConsumer<TMessage>>(message, targetMessageConsumer);

            throw new InvalidCastException($"type '{typeof(TMessageConsumer).Name}' is not instance of MessageConsumer '{typeof(TMessage).Name}'");
        }

        public MQTypedConsumerContext<TMessage, TContent, IMQTypedMessageConsumer<TMessage, TContent>> CreateTypedConsumerContext<TContent>(
            MQTypedMessage<TMessage, TContent> typedMessage)
            where TContent : class
        {
            var messageConsumer = _provider.GetRequiredService<TMessageConsumer>();
            if (messageConsumer == null)
                throw new NotImplementedException($"Unable to resolve message consumer type '{typeof(TMessageConsumer).Name}'");

            if (messageConsumer is IMQTypedMessageConsumer<TMessage, TContent> typedMessageConsumer)
                return new MQTypedConsumerContext<TMessage, TContent, IMQTypedMessageConsumer<TMessage, TContent>>(typedMessage, typedMessageConsumer);

            throw new InvalidCastException($"type '{typeof(TMessageConsumer).Name}' is not instance of TypedMessageConsumer <{typeof(TMessage).Name}, {typeof(TContent).Name}>");
        }
    }
}
