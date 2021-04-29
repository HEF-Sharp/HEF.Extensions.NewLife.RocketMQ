using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IMQProducerConfigurator
    {
        void Serialize(IMessageSerializer serializer);
    }

    public interface IRocketMQProducerConfigurator : IMQProducerConfigurator
    {
        void Configure(Action<Producer> configure);
    }
}
