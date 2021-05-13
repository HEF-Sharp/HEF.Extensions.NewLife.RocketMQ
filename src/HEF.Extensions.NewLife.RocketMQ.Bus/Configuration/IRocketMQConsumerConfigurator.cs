using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQConsumerConfigurator : IMQConsumerConfigurator<MessageExt>
    {
        void WithTags(params string[] tags);

        void Configure(Action<Consumer> configure);
    }
}
