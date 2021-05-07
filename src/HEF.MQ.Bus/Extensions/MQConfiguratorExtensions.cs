namespace HEF.MQ.Bus
{
    public static class MQConfiguratorExtensions
    {
        public static void BindMessageConsumer<TMessage, TMessageConsumer>(this IMQConsumerConfigurator<TMessage> configurator, IMQBusRegistration registration)
            where TMessage : class
            where TMessageConsumer : class, IMessageConsumer
        {
            registration.ConfigureMessageConsumer<TMessage, TMessageConsumer>(configurator);
        }

        public static void BindTypedMessageConsumer<TMessage, TContent, TTypedMessageConsumer>(this IMQConsumerConfigurator<TMessage> configurator,
            IMQBusRegistration registration)
            where TMessage : class
            where TContent : class
            where TTypedMessageConsumer : class, IMQTypedMessageConsumer<TMessage, TContent>
        {
            configurator.BindMessageConsumer<TMessage, TTypedMessageConsumer>(registration);
        }
    }
}
