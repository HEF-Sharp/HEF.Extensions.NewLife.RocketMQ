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
    public class RocketMQMessageJsonDeserializer : IMQMessageDeserializer<MessageExt>
    {
#if NETSTANDARD2_0
        private readonly JsonSerializerSettings _serializerSettings;

        public RocketMQMessageJsonDeserializer()
            : this(new JsonSerializerSettings())
        { }

        public RocketMQMessageJsonDeserializer(JsonSerializerSettings serializerSettings)
        {
            _serializerSettings = serializerSettings ?? throw new ArgumentNullException(nameof(serializerSettings));
        }

        public MQTypedMessage<MessageExt, TContent> Deserialize<TContent>(MessageExt message)
            where TContent : class
        {
            var content = JsonConvert.DeserializeObject<TContent>(message.BodyString, _serializerSettings);

            return new MQTypedMessage<MessageExt, TContent>
            {                
                Message = message,
                Content = content
            };
        }
#else
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
#endif
    }
}
