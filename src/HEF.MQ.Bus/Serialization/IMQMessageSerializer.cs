namespace HEF.MQ.Bus
{
    public interface IMQMessageSerializer<TMessage>
        where TMessage : class
    {
        TMessage Serialize<TContent>(MQTypedMessage<TMessage, TContent> typedMessage)            
            where TContent : class;
    }
}
