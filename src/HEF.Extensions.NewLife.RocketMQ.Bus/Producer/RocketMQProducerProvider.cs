using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQProducerProvider : IDisposable
    {
        IRocketMQTypedProducer GetTypedProducer(string topicName);
    }

    public class RocketMQProducerProvider : IRocketMQProducerProvider
    {
        private readonly RocketMQClientConfig _clientConfig;
        private readonly IReadOnlyDictionary<string, IRocketMQProducerSpecification> _producers;

        private Lazy<ProducerFactory> _producerFactory;
        private readonly ConcurrentDictionary<string, IRocketMQTypedProducer> _typedProducerCache = new();

        public RocketMQProducerProvider(RocketMQClientConfig clientConfig, IEnumerable<IRocketMQProducerSpecification> producers)
        {
            _clientConfig = clientConfig;
            _producers = producers.ToDictionary(m => m.TopicName);

            _producerFactory = new Lazy<ProducerFactory>(() => new ProducerFactory(_clientConfig.NameServer, _clientConfig.TopicQueueNum));
        }

        public IRocketMQTypedProducer GetTypedProducer(string topicName)
        {
            if (topicName.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(topicName));

            return _typedProducerCache.GetOrAdd(topicName, key => CreateTypedProducer(topicName));
        }

        #region Create TypedProducer
        private IRocketMQTypedProducer CreateTypedProducer(string topicName)
        {
            if (_producers.TryGetValue(topicName, out var producerSpec))
            {
                var typedProducerFactory = producerSpec.CreateTypedProducerFactory();
                return typedProducerFactory.Invoke(_producerFactory.Value);
            }

            return CreateDefaultTypedProducer(topicName);
        }

        private IRocketMQTypedProducer CreateDefaultTypedProducer(string topicName)
        {
            var topicProducer = _producerFactory.Value.GetTopicProducer(topicName);

            return new RocketMQTypedProducer(topicProducer, new RocketMQMessageJsonSerializer());
        }
        #endregion

        public void Dispose()
        {
            _typedProducerCache.Clear();

            if (_producerFactory.IsValueCreated)
            {
                _producerFactory.Value.Dispose();
            }
        }
    }
}
