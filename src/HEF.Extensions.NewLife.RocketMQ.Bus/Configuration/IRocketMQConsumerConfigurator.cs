using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IMQConsumerConfigurator
    {
        void Deserialize(IMessageDeserializer deserializer);
    }

    public interface IRocketMQConsumerConfigurator : IMQConsumerConfigurator
    {
        void WithTags(params string[] tags);

        void Configure(Action<Consumer> configure);
    }

    public interface IRocketMQConsumerConfigurator<TContent> : IRocketMQConsumerConfigurator
        where TContent : class
    {
    }
}
