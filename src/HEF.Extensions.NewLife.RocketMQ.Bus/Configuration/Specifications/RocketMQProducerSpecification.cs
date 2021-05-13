using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQProducerSpecification
    {
        string TopicName { get; }

        Func<ProducerFactory, IRocketMQTypedProducer> CreateTypedProducerFactory();
    }

    public class RocketMQProducerSpecification
        : IRocketMQProducerSpecification, IRocketMQProducerConfigurator
    {
        private Action<Producer> _producerConfigure;
        private IMQMessageSerializer<Message> _messageSerializer;

        public RocketMQProducerSpecification(string topicName)
        {
            TopicName = topicName;

            _messageSerializer = new RocketMQMessageJsonSerializer();
        }

        public string TopicName { get; }

        public void Serialize(IMQMessageSerializer<Message> serializer)
        {
            _messageSerializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public void Configure(Action<Producer> configure)
        {
            _producerConfigure = configure;
        }

        public Func<ProducerFactory, IRocketMQTypedProducer> CreateTypedProducerFactory()
        {
            return producerFactory =>
            {
                var topicProducer = producerFactory.GetTopicProducer(TopicName, _producerConfigure);

                return new RocketMQTypedProducer(topicProducer, _messageSerializer);
            };
        }
    }
}
