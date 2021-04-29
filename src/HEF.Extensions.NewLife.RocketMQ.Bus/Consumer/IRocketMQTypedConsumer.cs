using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQTypedConsumer<TContent> : IMessageConsumer
        where TContent : class
    {
        Task Consume(TypedMessageExt<TContent> typedMessage);
    }

    /// <summary>
    /// Marker interface used to assist identification in IoC containers
    /// </summary>
    public interface IMessageConsumer
    { }
}
