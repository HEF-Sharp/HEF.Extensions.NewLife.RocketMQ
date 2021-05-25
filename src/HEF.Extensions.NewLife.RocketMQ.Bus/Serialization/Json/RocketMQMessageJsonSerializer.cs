using System;
using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System.Text.Json;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQMessageJsonSerializer : IMQMessageSerializer<Message>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public RocketMQMessageJsonSerializer()
            : this(new JsonSerializerOptions())
        { }

        public RocketMQMessageJsonSerializer(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
        }

        public Message Serialize<TContent>(MQTypedMessage<Message, TContent> typedMessage)
            where TContent : class
        {
            typedMessage.Message.BodyString = JsonSerializer.Serialize(typedMessage.Content, _serializerOptions);

            return typedMessage.Message;
        }
    }
}
