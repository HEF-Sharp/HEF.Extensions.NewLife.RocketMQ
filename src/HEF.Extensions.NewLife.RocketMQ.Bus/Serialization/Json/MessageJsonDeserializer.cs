using System;
using NewLife.RocketMQ.Protocol;
#if NETSTANDARD2_0
using Newtonsoft.Json;
#else
using System.Text.Json;
#endif

namespace NewLife.RocketMQ.Bus
{
    public class MessageJsonDeserializer : IMessageDeserializer
    {
#if NETSTANDARD2_0
        private readonly JsonSerializerSettings _serializerSettings;

        public MessageJsonDeserializer()
            : this(new JsonSerializerSettings())
        { }

        public MessageJsonDeserializer(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
        }

        public TypedMessageExt<T> Deserialize<T>(MessageExt message)
            where T : class
        {
            var content = JsonConvert.DeserializeObject<T>(message.BodyString, _serializerSettings);

            return new TypedMessageExt<T>
            {
                Content = content,
                Message = message
            };
        }
#else
        private readonly JsonSerializerOptions _serializerOptions;

        public MessageJsonDeserializer()
            : this(new JsonSerializerOptions())
        { }

        public MessageJsonDeserializer(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions ?? throw new ArgumentNullException(nameof(serializerOptions));
        }

        public TypedMessageExt<T> Deserialize<T>(MessageExt message)
            where T : class
        {
            var content = JsonSerializer.Deserialize<T>(message.BodyString, _serializerOptions);

            return new TypedMessageExt<T>
            {
                Content = content,
                Message = message
            };
        }
#endif
    }
}
