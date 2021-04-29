using Microsoft.Extensions.Hosting;
using NewLife.RocketMQ.Bus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewLife.RocketMQ.AspNetCore
{
    public class RocketMQHostedService : IHostedService
    {
        private readonly IMQBusControl _rocketMQControl;

        public RocketMQHostedService(IMQBusControl rocketMQControl)
        {
            _rocketMQControl = rocketMQControl ?? throw new ArgumentNullException(nameof(rocketMQControl));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _rocketMQControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _rocketMQControl.StopAsync(cancellationToken);
        }
    }
}
