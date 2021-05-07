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
    public static class RocketMQConfiguratorExtensions
    {
#if NETSTANDARD2_0
        public static void JsonSerialize(this IRocketMQProducerConfigurator configurator, Action<JsonSerializerSettings> serializeConfigure)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializeConfigure?.Invoke(serializerSettings);

            configurator.Serialize(new RocketMQMessageJsonSerializer(serializerSettings));
        }

        public static void JsonDeserialize(this IRocketMQConsumerConfigurator configurator, Action<JsonSerializerSettings> serializeConfigure)           
        {
            var serializerSettings = new JsonSerializerSettings();
            serializeConfigure?.Invoke(serializerSettings);

            configurator.Deserialize(new RocketMQMessageJsonDeserializer(serializerSettings));
        }
#else
        public static void JsonSerialize(this IRocketMQProducerConfigurator configurator, Action<JsonSerializerOptions> serializeConfigure)
        {
            var serializerOptions = new JsonSerializerOptions();
            serializeConfigure?.Invoke(serializerOptions);

            configurator.Serialize(new RocketMQMessageJsonSerializer(serializerOptions));
        }

        public static void JsonDeserialize(this IRocketMQConsumerConfigurator configurator, Action<JsonSerializerOptions> serializeConfigure)
        {
            var serializerOptions = new JsonSerializerOptions();
            serializeConfigure?.Invoke(serializerOptions);

            configurator.Deserialize(new RocketMQMessageJsonDeserializer(serializerOptions));
        }
#endif
        public static void BindMessageConsumer<TMessageConsumer>(this IRocketMQConsumerConfigurator configurator, IMQBusRegistration registration)            
            where TMessageConsumer : class, IMessageConsumer
        {
            configurator.BindMessageConsumer<MessageExt, TMessageConsumer>(registration);
        }

        public static void BindTypedMessageConsumer<TContent, TTypedMessageConsumer>(this IRocketMQConsumerConfigurator configurator,
            IMQBusRegistration registration)            
            where TContent : class
            where TTypedMessageConsumer : class, IMQTypedMessageConsumer<MessageExt, TContent>
        {
            configurator.BindTypedMessageConsumer<MessageExt, TContent, TTypedMessageConsumer>(registration);
        }
    }
}
