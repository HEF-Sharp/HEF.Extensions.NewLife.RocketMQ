using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQProducerConfigurator : IMQProducerConfigurator<Message>
    {
        void Configure(Action<Producer> configure);
    }
}
