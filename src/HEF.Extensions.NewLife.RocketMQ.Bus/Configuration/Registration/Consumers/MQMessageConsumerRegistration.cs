using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IMQMessageConsumerRegistration
    {
        Type MessageConsumerType { get; }

        void BindConsumer(IMQConsumerConfigurator configurator);
    }

    public class MQMessageConsumerRegistration<TMessageConsumer> : IMQMessageConsumerRegistration
        where TMessageConsumer : class, IMessageConsumer
    {
        public Type MessageConsumerType => typeof(TMessageConsumer);

        public void BindConsumer(IMQConsumerConfigurator configurator)
        {
            throw new NotImplementedException();
        }
    }
}
