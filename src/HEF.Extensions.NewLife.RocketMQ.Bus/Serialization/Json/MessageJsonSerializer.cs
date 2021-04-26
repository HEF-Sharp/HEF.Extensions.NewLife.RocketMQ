using System;
using NewLife.RocketMQ.Protocol;
#if NETSTANDARD2_0
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif

namespace NewLife.RocketMQ.Bus
{
    public class MessageJsonSerializer : IMessageSerializer
    {
#if NETSTANDARD2_0
        private readonly JsonSerializerSettings _serializerSettings;

        public MessageJsonSerializer()
            : this(new JsonSerializerSettings())
        { }

        public MessageJsonSerializer(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
        }

        public Message Serialize<T>(TypedMessage<T> typedMessage)
            where T : class
        {
            typedMessage.Message.BodyString = JsonConvert.SerializeObject(typedMessage.Content, _serializerSettings);

            return typedMessage.Message;
        }
#else
        private readonly JsonSerializerOptions _serializerOptions;

        public MessageJsonSerializer()
            : this(new JsonSerializerOptions())
        { }

        public MessageJsonSerializer(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
        }

        public Message Serialize<T>(TypedMessage<T> typedMessage)
            where T : class
        {
            typedMessage.Message.BodyString = JsonSerializer.Serialize(typedMessage.Content, _serializerOptions);

            return typedMessage.Message;
        }
#endif
    }
}
