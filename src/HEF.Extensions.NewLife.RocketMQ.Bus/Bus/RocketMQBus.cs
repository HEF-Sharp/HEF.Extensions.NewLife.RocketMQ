using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQBus : IRocketMQControl
    {
        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
