using System;
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

            configurator.Serialize(new MessageJsonSerializer(serializerSettings));
        }

        public static void JsonDeserialize(this IRocketMQConsumerConfigurator configurator, Action<JsonSerializerSettings> serializeConfigure)           
        {
            var serializerSettings = new JsonSerializerSettings();
            serializeConfigure?.Invoke(serializerSettings);

            configurator.Deserialize(new MessageJsonDeserializer(serializerSettings));
        }
#else
        public static void JsonSerialize(this IRocketMQProducerConfigurator configurator, Action<JsonSerializerOptions> serializeConfigure)
        {
            var serializerOptions = new JsonSerializerOptions();
            serializeConfigure?.Invoke(serializerOptions);

            configurator.Serialize(new MessageJsonSerializer(serializerOptions));
        }

        public static void JsonDeserialize(this IRocketMQConsumerConfigurator configurator, Action<JsonSerializerOptions> serializeConfigure)
        {
            var serializerOptions = new JsonSerializerOptions();
            serializeConfigure?.Invoke(serializerOptions);

            configurator.Deserialize(new MessageJsonDeserializer(serializerOptions));
        }
#endif
        public static void BindMessageConsumer<TMessageConsumer>(this IMQConsumerConfigurator configurator, IMQBusRegistration registration)
            where TMessageConsumer : class, IMessageConsumer
        {
            registration.ConfigureMessageConsumer<TMessageConsumer>(configurator);
        }

        public static void BindTypedConsumer<TContent, TTypedConsumer>(this IRocketMQConsumerConfigurator<TContent> configurator,
            IMQBusRegistration registration)
            where TContent : class
            where TTypedConsumer : class, IRocketMQTypedConsumer<TContent>
        {
            configurator.BindMessageConsumer<TTypedConsumer>(registration);
        }
    }
}
