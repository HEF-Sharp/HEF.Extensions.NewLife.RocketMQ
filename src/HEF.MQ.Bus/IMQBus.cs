using System.Threading;
using System.Threading.Tasks;

namespace HEF.MQ.Bus
{
    public interface IMQBusControl
    {
        Task StartAsync(CancellationToken cancellationToken = default);

        Task StopAsync(CancellationToken cancellationToken = default);
    }

    public interface IMQBus<out TProducerProvider> : IMQBusControl
        where TProducerProvider : class
    {
        TProducerProvider ProducerProvider { get; }
    }
}
