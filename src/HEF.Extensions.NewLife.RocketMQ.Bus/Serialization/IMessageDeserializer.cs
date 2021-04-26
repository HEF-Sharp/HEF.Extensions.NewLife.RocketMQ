using NewLife.RocketMQ.Protocol;

namespace NewLife.RocketMQ.Bus
{
    public interface IMessageDeserializer
    {
        TypedMessageExt<T> Deserialize<T>(MessageExt message) where T : class;
    }
}
