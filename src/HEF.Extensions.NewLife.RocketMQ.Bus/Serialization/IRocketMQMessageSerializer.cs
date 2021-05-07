using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQMessageSerializer : IMQMessageSerializer<Message>
    {
    }
}
