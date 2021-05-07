using System;

namespace HEF.MQ.Bus
{
    public interface IMQMessageConsumerRegistration
    {
        Type MessageConsumerType { get; }

        void AttachToConsumer<TMessage>(IMQConsumerConfigurator<TMessage> configurator, IMQServiceProvider provider)
            where TMessage : class;
    }

    public class MQMessageConsumerRegistration<TMessageConsumer> : IMQMessageConsumerRegistration
        where TMessageConsumer : class, IMessageConsumer
    {
        public Type MessageConsumerType => typeof(TMessageConsumer);

        public void AttachToConsumer<TMessage>(IMQConsumerConfigurator<TMessage> configurator, IMQServiceProvider provider)
            where TMessage : class
        {
            throw new NotImplementedException();
        }
    }
}
