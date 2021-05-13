using System;
using System.Threading.Tasks;

namespace HEF.MQ.Bus
{
    public interface IMQMessageConsumeExecutor<TMessage>
        where TMessage : class
    {
        Task<bool> Execute(TMessage message);
    }

    public class MQMessageConsumeExecutor<TMessage, TMessageConsumer> : IMQMessageConsumeExecutor<TMessage>
        where TMessage : class
        where TMessageConsumer : class, IMQMessageConsumer
    {
        private static IMQMessageConsumer<TMessage> _emptyMessageConsumer = new DoNothingMQMessageConsumer<TMessage>();

        private readonly IMQServiceProvider _provider;
        private Lazy<IMQMessageConsumer<TMessage>> _messageConsumer;

        public MQMessageConsumeExecutor(IMQServiceProvider provider)
        {
            _provider = provider;

            _messageConsumer = new Lazy<IMQMessageConsumer<TMessage>>(ResolveMessageConsumer);
        }

        public Task<bool> Execute(TMessage message)
        {
            var consumerContext = CreateConsumerContext(message);

            return consumerContext.MessageConsumer.Consume(consumerContext.Message);
        }

        #region Helper Functions
        private IMQMessageConsumer<TMessage> ResolveMessageConsumer()
        {
            var messageConsumer = _provider.GetRequiredService<TMessageConsumer>();
            if (messageConsumer == null)
                throw new NotImplementedException($"Unable to resolve message consumer type '{typeof(TMessageConsumer).Name}'");

            if (messageConsumer is IMQMessageConsumer<TMessage> targetMessageConsumer)
                return targetMessageConsumer;

            return _emptyMessageConsumer;
        }

        private MQConsumerContext<TMessage, IMQMessageConsumer<TMessage>> CreateConsumerContext(TMessage message)
        {
            return new MQConsumerContext<TMessage, IMQMessageConsumer<TMessage>>(message, _messageConsumer.Value);
        }
        #endregion
    }

    public class MQTypedMessageConsumeExecutor<TMessage, TContent, TMessageConsumer> : IMQMessageConsumeExecutor<TMessage>
        where TMessage : class
        where TContent : class
        where TMessageConsumer : class, IMQMessageConsumer
    {
        private static IMQTypedMessageConsumer<TMessage, TContent> _emptyTypedMessageConsumer
            = new DoNothingMQTypedMessageConsumer<TMessage, TContent>();

        private readonly IMQServiceProvider _provider;
        private readonly IMQMessageDeserializer<TMessage> _deserializer;
        private Lazy<IMQTypedMessageConsumer<TMessage, TContent>> _typedMessageConsumer;

        public MQTypedMessageConsumeExecutor(IMQServiceProvider provider, IMQMessageDeserializer<TMessage> deserializer)
        {
            _provider = provider;
            _deserializer = deserializer;

            _typedMessageConsumer = new Lazy<IMQTypedMessageConsumer<TMessage, TContent>>(ResolveTypedMessageConsumer);
        }

        public Task<bool> Execute(TMessage message)
        {
            var typedMessage = _deserializer.Deserialize<TContent>(message);

            var consumerContext = CreateConsumerContext(typedMessage);

            return consumerContext.TypedMessageConsumer.Consume(consumerContext.TypedMessage);
        }

        #region Helper Functions
        private IMQTypedMessageConsumer<TMessage, TContent> ResolveTypedMessageConsumer()
        {
            var messageConsumer = _provider.GetRequiredService<TMessageConsumer>();
            if (messageConsumer == null)
                throw new NotImplementedException($"Unable to resolve message consumer type '{typeof(TMessageConsumer).Name}'");

            if (messageConsumer is IMQTypedMessageConsumer<TMessage, TContent> typedMessageConsumer)
                return typedMessageConsumer;

            return _emptyTypedMessageConsumer;
        }

        private MQTypedConsumerContext<TMessage, TContent, IMQTypedMessageConsumer<TMessage, TContent>> CreateConsumerContext(
            MQTypedMessage<TMessage, TContent> typedMessage)
        {
            return new MQTypedConsumerContext<TMessage, TContent, IMQTypedMessageConsumer<TMessage, TContent>>(
                typedMessage, _typedMessageConsumer.Value);
        }
        #endregion
    }
}
