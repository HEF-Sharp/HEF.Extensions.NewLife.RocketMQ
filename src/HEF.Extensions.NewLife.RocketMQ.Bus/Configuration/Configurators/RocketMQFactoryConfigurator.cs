using HEF.MQ.Bus;
using System;
using System.Collections.ObjectModel;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQFactoryConfigurator : IRocketMQFactoryConfigurator
    {
        private readonly RocketMQClientConfig _clientConfig = new();
        private readonly Collection<IRocketMQProducerSpecification> _producers = new();
        private readonly Collection<IRocketMQConsumerSpecification> _consumers = new();

        private bool _isNameServerConfigured = false;

        public void NameServer(string nameServerAddress)
        {
            if (nameServerAddress.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(nameServerAddress));

            if (_isNameServerConfigured)
                throw new InvalidOperationException("NameServer may not be specified more than once.");
            _isNameServerConfigured = true;

            _clientConfig.NameServer = nameServerAddress;
        }

        public void TopicCreate(int topicQueueNum)
        {
            _clientConfig.TopicQueueNum = topicQueueNum;
        }

        public void TopicProducer(string topicName, Action<IRocketMQProducerConfigurator> configure)
        {
            if (topicName.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(topicName));

            var producerSpec = new RocketMQProducerSpecification(topicName);
            configure?.Invoke(producerSpec);

            _producers.Add(producerSpec);
        }

        public void TopicConsumer(string topicName, string group, Action<IRocketMQConsumerConfigurator> configure)            
        {
            if (topicName.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(topicName));
            if (group.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(group));

            var consumerSpec = new RocketMQConsumerSpecification(topicName, group);
            configure?.Invoke(consumerSpec);

            _consumers.Add(consumerSpec);
        }

        public IMQBus Build(IMQBusRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}
