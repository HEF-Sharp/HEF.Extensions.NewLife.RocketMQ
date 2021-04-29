using NewLife.RocketMQ.Protocol;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQTypedProducer
    {
        SendResult Publish<TContent>(TContent content) where TContent : class;

        SendResult Publish<TContent>(TContent content, string tag) where TContent : class;

        SendResult Publish<TContent>(TContent content, string tag, string key) where TContent : class;
    }
}
