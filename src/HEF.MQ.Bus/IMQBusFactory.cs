namespace HEF.MQ.Bus
{
    public interface IMQBusFactory
    {
        IMQBus CreateMQBus(IMQBusRegistration registration);
    }
}
