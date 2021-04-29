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
        private IMessageSerializer _messageSerializer;

        public RocketMQProducerSpecification(string topicName)
        {
            TopicName = topicName;
        }

        public string TopicName { get; }        

        public void Serialize(IMessageSerializer serializer)
        {
            _messageSerializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public void Configure(Action<Producer> configure)
        {
            _producerConfigure = configure;
        }
    }
}
