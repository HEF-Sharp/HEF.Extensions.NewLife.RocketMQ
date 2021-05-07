namespace HEF.MQ.Bus
{
    public interface IMQMessageDeserializer<TMessage>
        where TMessage : class
    {
        MQTypedMessage<TMessage, TContent> Deserialize<TContent>(TMessage message)
            where TContent : class;
    }
}
