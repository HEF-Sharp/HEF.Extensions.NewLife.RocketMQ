using System;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQProducerSpecification
        : IRocketMQProducerSpecification, IRocketMQProducerConfigurator
    {
        private Action<Producer> _producerConfigure;
        private IMessageSerializer _messageSerializer;        

        public RocketMQProducerSpecification(string topicName)
        {
            TopicName = topicName;
        }

        public String TopicName { get; }

        public void Configure(Action<Producer> configure)
        {
            _producerConfigure = configure;
        }

        public void Serialize(IMessageSerializer serializer)
        {
            _messageSerializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }
    }
}
