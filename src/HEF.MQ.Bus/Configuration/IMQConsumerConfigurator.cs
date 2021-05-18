using System;

namespace HEF.MQ.Bus
{
    public interface IMQConsumerConfigurator<TMessage>
        where TMessage : class
    {
        void Deserialize(IMQMessageDeserializer<TMessage> deserializer);

        void Consume(Func<IMQMessageConsumeExecutor<TMessage>> executorFactory);

        void TypedConsume(Func<IMQMessageDeserializer<TMessage>, IMQMessageConsumeExecutor<TMessage>> executorFactory);
    }
}
