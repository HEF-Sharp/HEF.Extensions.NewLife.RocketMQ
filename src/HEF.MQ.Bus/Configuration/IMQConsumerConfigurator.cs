namespace HEF.MQ.Bus
{
    public interface IMQConsumerConfigurator<TMessage>
        where TMessage : class
    {
        void Deserialize(IMQMessageDeserializer<TMessage> deserializer);
    }
}
