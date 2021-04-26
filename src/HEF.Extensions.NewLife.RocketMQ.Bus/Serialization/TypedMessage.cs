using NewLife.RocketMQ.Protocol;

namespace NewLife.RocketMQ.Bus
{
    public class TypedMessage<TContent>
        where TContent : class
    {
        public TContent Content { get; set; }

        public Message Message { get; set; }
    }

    public class TypedMessageExt<TContent>
        where TContent : class
    {
        public TContent Content { get; set; }

        public MessageExt Message { get; set; }
    }
}
