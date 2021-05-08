namespace HEF.MQ.Bus
{
    public static class MQConfiguratorExtensions
    {
        public static void BindMessageConsumer<TMessage, TMessageConsumer>(this IMQConsumerConfigurator<TMessage> configurator, IMQBusRegistration registration)
            where TMessage : class
            where TMessageConsumer : class, IMQMessageConsumer
        {
            registration.ConfigureMessageConsumer<TMessage, TMessageConsumer>(configurator);
        }

        public static void BindTypedMessageConsumer<TMessage, TContent, TMessageConsumer>(this IMQConsumerConfigurator<TMessage> configurator,
            IMQBusRegistration registration)
            where TMessage : class
            where TContent : class
            where TMessageConsumer : class, IMQMessageConsumer
        {
            registration.ConfigureTypedMessageConsumer<TMessage, TContent, TMessageConsumer>(configurator);
        }
    }
}
