using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQProducerSpecification
    {
        string TopicName { get; }
    }

    public class RocketMQProducerSpecification
        : IRocketMQProducerSpecification, IRocketMQProducerConfigurator
    {
        private Action<Producer> _producerConfigure;
        private IMQMessageSerializer<Message> _messageSerializer;

        public RocketMQProducerSpecification(string topicName)
        {
            TopicName = topicName;
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
    }
}
