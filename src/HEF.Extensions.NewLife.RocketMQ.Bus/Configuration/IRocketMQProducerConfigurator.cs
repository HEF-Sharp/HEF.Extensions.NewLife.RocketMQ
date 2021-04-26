using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQProducerConfigurator
    {
        void Configure(Action<Producer> configure);

        void Serialize(IMessageSerializer serializer);
    }
}
