using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQTypedProducer : IRocketMQTypedProducer
    {
        private readonly Producer _innerProducer;
        private readonly IMessageSerializer _messageSerializer;

        public RocketMQTypedProducer(Producer producer, IMessageSerializer messageSerializer)
        {
            if (producer == null)
                throw new ArgumentNullException(nameof(producer));
            if (messageSerializer == null)
                throw new ArgumentNullException(nameof(messageSerializer));

            _innerProducer = producer;
            _messageSerializer = messageSerializer;
        }

        public SendResult Publish<TContent>(TContent content) where TContent : class
            => Publish(content, null);

        public SendResult Publish<TContent>(TContent content, string tag) where TContent : class
            => Publish(content, tag, null);        

        public SendResult Publish<TContent>(TContent content, string tag, string key) where TContent : class
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            var typedMessage = new TypedMessage<TContent>
            {
                Content = content,
                Message = new Message { Tags = tag, Keys = key }
            };

            var message = _messageSerializer.Serialize(typedMessage);

            return _innerProducer.Publish(message);
        }
    }
}
