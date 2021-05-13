using HEF.MQ.Bus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQBus : IMQBus
    {
        private readonly IMQBusRegistration _registration;        

        public RocketMQBus(IMQBusRegistration registration)
        {
            _registration = registration;
        }

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
