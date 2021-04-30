using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IMQMessageConsumerRegistration
    {
        Type MessageConsumerType { get; }

        void AttachToConsumer(IMQConsumerConfigurator configurator, IMQServiceProvider provider);
    }

    public class MQMessageConsumerRegistration<TMessageConsumer> : IMQMessageConsumerRegistration
        where TMessageConsumer : class, IMessageConsumer
    {
        public Type MessageConsumerType => typeof(TMessageConsumer);

        public void AttachToConsumer(IMQConsumerConfigurator configurator, IMQServiceProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
