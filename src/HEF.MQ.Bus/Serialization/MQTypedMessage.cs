namespace HEF.MQ.Bus
{
    public class MQTypedMessage<TMessage, TContent>
        where TMessage : class
        where TContent : class
    {
        public TMessage Message { get; set; }

        public TContent Content { get; set; }
    }
}
