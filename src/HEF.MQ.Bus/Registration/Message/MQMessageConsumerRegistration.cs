using System;

namespace HEF.MQ.Bus
{
    public interface IMQMessageConsumerRegistration
    {
        Type MessageConsumerType { get; }

        void AttachToConsumer<TMessage>(IMQConsumerConfigurator<TMessage> configurator, IMQServiceProvider provider)
            where TMessage : class;

        void AttachToConsumer<TMessage, TContent>(IMQConsumerConfigurator<TMessage> configurator, IMQServiceProvider provider)
            where TMessage : class
            where TContent : class;
    }

    public class MQMessageConsumerRegistration<TMessageConsumer> : IMQMessageConsumerRegistration
        where TMessageConsumer : class, IMQMessageConsumer
    {
        public Type MessageConsumerType => typeof(TMessageConsumer);

        public void AttachToConsumer<TMessage>(IMQConsumerConfigurator<TMessage> configurator, IMQServiceProvider provider)
            where TMessage : class
        {
            throw new NotImplementedException();
        }

        public void AttachToConsumer<TMessage, TContent>(IMQConsumerConfigurator<TMessage> configurator, IMQServiceProvider provider)
            where TMessage : class
            where TContent : class
        {
            throw new NotImplementedException();
        }
    }
}
