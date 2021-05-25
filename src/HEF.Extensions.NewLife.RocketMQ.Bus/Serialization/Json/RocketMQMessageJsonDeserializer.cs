using System;
using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System.Text.Json;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQMessageJsonDeserializer : IMQMessageDeserializer<MessageExt>
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public RocketMQMessageJsonDeserializer()
            : this(new JsonSerializerOptions())
        { }

        public RocketMQMessageJsonDeserializer(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
        }

        public MQTypedMessage<MessageExt, TContent> Deserialize<TContent>(MessageExt message)
            where TContent : class
        {
            var content = JsonSerializer.Deserialize<TContent>(message.BodyString, _serializerOptions);

            return new MQTypedMessage<MessageExt, TContent>
            {
                Message = message,
                Content = content
            };
        }
    }
}
