namespace HEF.MQ.Bus
{
    public interface IMQProducerConfigurator<TMessage>
        where TMessage : class
    {
        void Serialize(IMQMessageSerializer<TMessage> serializer);
    }
}
