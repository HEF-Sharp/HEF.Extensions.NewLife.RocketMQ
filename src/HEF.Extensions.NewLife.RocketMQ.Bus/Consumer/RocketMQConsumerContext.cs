using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQConsumerContext<TContent, TMessageConsumer>
        : MQTypedConsumerContext<MessageExt, TContent, TMessageConsumer>
        where TContent : class
        where TMessageConsumer : class, IMQTypedMessageConsumer<MessageExt, TContent>
    {
        public RocketMQConsumerContext(MQTypedMessage<MessageExt, TContent> typedMessage, TMessageConsumer typedMessageConsumer)
            : base(typedMessage, typedMessageConsumer)
        { }
    }
}
