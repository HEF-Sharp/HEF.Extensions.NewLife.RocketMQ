using System;
using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System.Text.Json;

namespace NewLife.RocketMQ.Bus
{
    public static class RocketMQConfiguratorExtensions
    {
        public static void JsonSerialize(this IRocketMQProducerConfigurator configurator, Action<JsonSerializerOptions> serializeConfigure = null)
        {
            var serializerOptions = new JsonSerializerOptions();
            serializeConfigure?.Invoke(serializerOptions);

            configurator.Serialize(new RocketMQMessageJsonSerializer(serializerOptions));
        }

        public static void JsonDeserialize(this IRocketMQConsumerConfigurator configurator, Action<JsonSerializerOptions> serializeConfigure = null)
        {
            var serializerOptions = new JsonSerializerOptions();
            serializeConfigure?.Invoke(serializerOptions);

            configurator.Deserialize(new RocketMQMessageJsonDeserializer(serializerOptions));
        }

        public static void BindMessageConsumer<TMessageConsumer>(this IRocketMQConsumerConfigurator configurator, IMQBusRegistration registration)            
            where TMessageConsumer : class, IMQMessageConsumer
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
