using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQConsumerConfigurator
    {
        void Configure(Action<Consumer> configure);

        void Deserialize(IMessageDeserializer deserializer);
    }
}
