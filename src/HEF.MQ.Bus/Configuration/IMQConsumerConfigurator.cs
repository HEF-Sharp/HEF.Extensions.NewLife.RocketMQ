using System;

namespace HEF.MQ.Bus
{
    public interface IMQConsumerConfigurator<TMessage>
        where TMessage : class
    {
        void Deserialize(IMQMessageDeserializer<TMessage> deserializer);

        void Consume(IMQMessageConsumeExecutor<TMessage> executer);

        void TypedConsume(Func<IMQMessageDeserializer<TMessage>, IMQMessageConsumeExecutor<TMessage>> executerFactory);
    }
}
