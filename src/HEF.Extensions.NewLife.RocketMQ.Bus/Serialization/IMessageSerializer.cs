using NewLife.RocketMQ.Protocol;

namespace NewLife.RocketMQ.Bus
{
    public interface IMessageSerializer
    {
        Message Serialize<T>(TypedMessage<T> typedMessage) where T : class;
    }
}
