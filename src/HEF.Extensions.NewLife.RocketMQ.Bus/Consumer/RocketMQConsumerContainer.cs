using System;
using System.Collections.Generic;
using System.Linq;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQConsumerContainer : IDisposable
    {
        void StartConsume();
    }

    public class RocketMQConsumerContainer : IRocketMQConsumerContainer
    {
        private readonly RocketMQClientConfig _clientConfig;
        private readonly IReadOnlyCollection<IRocketMQConsumerSpecification> _consumers;

        private Lazy<ConsumerContainer> _consumerContainer;

        public RocketMQConsumerContainer(RocketMQClientConfig clientConfig, IEnumerable<IRocketMQConsumerSpecification> consumers)
        {
            _clientConfig = clientConfig;
            _consumers = consumers.ToList().AsReadOnly();

            _consumerContainer = new Lazy<ConsumerContainer>(() => new ConsumerContainer(_clientConfig.NameServer));
        }

        public void StartConsume()
        {
            foreach (var consumerSpec in _consumers)
            {
                var addTopicConsumerAction = consumerSpec.CreateAddTopicConsumerAction();

                addTopicConsumerAction.Invoke(_consumerContainer.Value);
            }
        }

        public void Dispose()
        {
            if (_consumerContainer.IsValueCreated)
            {
                _consumerContainer.Value.Dispose();
            }
        }
    }
}
