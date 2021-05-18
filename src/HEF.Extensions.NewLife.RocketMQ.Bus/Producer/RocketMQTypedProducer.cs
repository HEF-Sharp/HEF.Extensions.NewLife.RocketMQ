using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQTypedProducer
    {
        SendResult Publish<TContent>(TContent content, Action<IRocketMQMessageBuilder> msgConfigure = null) where TContent : class;
    }

    public class RocketMQTypedProducer : IRocketMQTypedProducer
    {
        private readonly Producer _innerProducer;
        private readonly IMQMessageSerializer<Message> _messageSerializer;

        public RocketMQTypedProducer(Producer producer, IMQMessageSerializer<Message> messageSerializer)
        {
            if (producer == null)
                throw new ArgumentNullException(nameof(producer));
            if (messageSerializer == null)
                throw new ArgumentNullException(nameof(messageSerializer));

            _innerProducer = producer;
            _messageSerializer = messageSerializer;
        }

        public SendResult Publish<TContent>(TContent content, Action<IRocketMQMessageBuilder> msgConfigure = null)
            where TContent : class
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            var messageBuilder = new RocketMQMessageBuilder();
            msgConfigure?.Invoke(messageBuilder);

            var typedMessage = new MQTypedMessage<Message, TContent>
            {
                Content = content,
                Message = messageBuilder.Build()
            };

            var message = _messageSerializer.Serialize(typedMessage);

            return _innerProducer.Publish(message);
        }
    }
}
