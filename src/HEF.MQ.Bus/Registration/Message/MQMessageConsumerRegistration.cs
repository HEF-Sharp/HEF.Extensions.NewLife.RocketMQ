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
            CheckIsMessageConsumer<TMessage>();

            configurator.Consume(() => new MQMessageConsumeExecutor<TMessage, TMessageConsumer>(provider));
        }

        public void AttachToConsumer<TMessage, TContent>(IMQConsumerConfigurator<TMessage> configurator, IMQServiceProvider provider)
            where TMessage : class
            where TContent : class
        {
            CheckIsTypedMessageConsumer<TMessage, TContent>();

            configurator.TypedConsume(deserializer =>
            {
                return new MQTypedMessageConsumeExecutor<TMessage, TContent, TMessageConsumer>(provider, deserializer);
            });
        }

        #region Helper Functions
        private void CheckIsMessageConsumer<TMessage>()
            where TMessage : class
        {
            if (!typeof(IMQMessageConsumer<TMessage>).IsAssignableFrom(MessageConsumerType))
                throw new InvalidCastException($"message consumer type '{MessageConsumerType.Name}' can not consume message of '{typeof(TMessage).Name}'");
        }

        private void CheckIsTypedMessageConsumer<TMessage, TContent>()
            where TMessage : class
            where TContent : class
        {
            if (!typeof(IMQTypedMessageConsumer<TMessage, TContent>).IsAssignableFrom(MessageConsumerType))
                throw new InvalidCastException($"message consumer type '{MessageConsumerType.Name}' can not consume typed message of <{typeof(TMessage).Name}, {typeof(TContent).Name}>");
        }
        #endregion
    }
}
