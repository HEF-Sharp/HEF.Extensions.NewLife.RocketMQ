using HEF.MQ.Bus;
using System.Threading;
using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQBus : IMQBus<IRocketMQProducerProvider>
    {
        private readonly IMQBusRegistration _registration;

        private readonly IRocketMQProducerProvider _producerProvider;
        private readonly IRocketMQConsumerContainer _consumerContainer;

        public RocketMQBus(IMQBusRegistration registration,
            IRocketMQProducerProvider producerProvider, IRocketMQConsumerContainer consumerContainer)
        {
            _registration = registration;

            _producerProvider = producerProvider;
            _consumerContainer = consumerContainer;
        }

        public IRocketMQProducerProvider ProducerProvider => _producerProvider;

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            await Task.Yield();

            _consumerContainer.StartConsume();
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await Task.Yield();

            _producerProvider.Dispose();

            _consumerContainer.Dispose();
        }
    }
}
