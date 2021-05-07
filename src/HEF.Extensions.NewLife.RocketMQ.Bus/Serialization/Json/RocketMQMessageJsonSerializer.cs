using System;
using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
#if NETSTANDARD2_0
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQMessageJsonSerializer : IRocketMQMessageSerializer
    {
#if NETSTANDARD2_0
        private readonly JsonSerializerSettings _serializerSettings;

        public RocketMQMessageJsonSerializer()
            : this(new JsonSerializerSettings())
        { }

        public RocketMQMessageJsonSerializer(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
        }

        public Message Serialize<TContent>(MQTypedMessage<Message, TContent> typedMessage)
            where TContent : class
        {
            typedMessage.Message.BodyString = JsonConvert.SerializeObject(typedMessage.Content, _serializerSettings);

            return typedMessage.Message;
        }
#else
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
#endif
    }
}
