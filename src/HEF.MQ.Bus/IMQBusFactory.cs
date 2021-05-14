namespace HEF.MQ.Bus
{
    public interface IMQBusFactory<TProducerProvider>
        where TProducerProvider : class
    {
        IMQBus<TProducerProvider> CreateMQBus(IMQBusRegistration registration);
    }
}
