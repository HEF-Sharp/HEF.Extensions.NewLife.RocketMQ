using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQTypedMessageConsumer<TContent> : IMessageConsumer
        where TContent : class
    {
        Task<bool> Consume(TypedMessageExt<TContent> typedMessage);
    }

    /// <summary>
    /// Marker interface used to assist identification in IoC containers
    /// </summary>
    public interface IMessageConsumer
    { }
}
