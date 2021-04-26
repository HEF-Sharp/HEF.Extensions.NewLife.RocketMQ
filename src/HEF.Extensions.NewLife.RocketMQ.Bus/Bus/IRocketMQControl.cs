using System.Threading;
using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{    
    public interface IRocketMQControl
    {
        Task StartAsync(CancellationToken cancellationToken = default);
        
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
