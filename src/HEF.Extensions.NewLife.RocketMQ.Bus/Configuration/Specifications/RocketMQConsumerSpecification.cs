using System;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQConsumerSpecification
        : IRocketMQConsumerSpecification, IRocketMQConsumerConfigurator
    {
        private Action<Consumer> _consumerConfigure;
        private IMessageDeserializer _messageDeserializer;

        public RocketMQConsumerSpecification(string topicName, string group)
        {
            TopicName = topicName;
            Group = group;
        }

        public String TopicName { get; }

        public String Group { get; }

        public void Configure(Action<Consumer> configure)
        {
            _consumerConfigure = configure;
        }

        public void Deserialize(IMessageDeserializer deserializer)
        {
            _messageDeserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }
    }
}
