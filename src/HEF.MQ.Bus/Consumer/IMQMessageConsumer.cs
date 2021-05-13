using System.Threading.Tasks;

namespace HEF.MQ.Bus
{
    /// <summary>
    /// Marker interface used to assist identification in IoC containers
    /// </summary>
    public interface IMQMessageConsumer
    { }

    public interface IMQMessageConsumer<TMessage> : IMQMessageConsumer
        where TMessage : class
    {
        Task<bool> Consume(TMessage message);
    }

    public interface IMQTypedMessageConsumer<TMessage, TContent> : IMQMessageConsumer
        where TMessage : class
        where TContent : class
    {
        Task<bool> Consume(MQTypedMessage<TMessage, TContent> typedMessage);
    }

    public class DoNothingMQMessageConsumer<TMessage> : IMQMessageConsumer<TMessage>
        where TMessage : class
    {
        public Task<bool> Consume(TMessage message)
        {
            return Task.FromResult(true);
        }
    }

    public class DoNothingMQTypedMessageConsumer<TMessage, TContent> : IMQTypedMessageConsumer<TMessage, TContent>
        where TMessage : class
        where TContent : class
    {
        public Task<bool> Consume(MQTypedMessage<TMessage, TContent> typedMessage)
        {
            return Task.FromResult(true);
        }
    }
}
