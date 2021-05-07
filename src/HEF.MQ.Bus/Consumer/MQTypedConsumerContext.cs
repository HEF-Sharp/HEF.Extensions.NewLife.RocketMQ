namespace HEF.MQ.Bus
{
    public class MQTypedConsumerContext<TMessage, TContent, TMessageConsumer>        
        where TMessage : class
        where TContent : class
        where TMessageConsumer : class, IMQTypedMessageConsumer<TMessage, TContent>
    {
        public MQTypedConsumerContext(MQTypedMessage<TMessage, TContent> typedMessage, TMessageConsumer typedMessageConsumer)            
        {
            TypedMessage = typedMessage;
            TypedMessageConsumer = typedMessageConsumer;
        }

        public MQTypedMessage<TMessage, TContent> TypedMessage { get; }

        public TMessageConsumer TypedMessageConsumer { get; }

        public TMessage Message => TypedMessage.Message;

        public TContent Content => TypedMessage.Content;
    }
}
