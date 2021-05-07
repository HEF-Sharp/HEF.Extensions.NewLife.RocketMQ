using System.Threading.Tasks;

namespace HEF.MQ.Bus
{
    /// <summary>
    /// Marker interface used to assist identification in IoC containers
    /// </summary>
    public interface IMessageConsumer
    { }

    public interface IMQTypedMessageConsumer<TMessage, TContent> : IMessageConsumer
        where TMessage : class
        where TContent : class
    {
        Task<bool> Consume(MQTypedMessage<TMessage, TContent> typedMessage);
    }
}
