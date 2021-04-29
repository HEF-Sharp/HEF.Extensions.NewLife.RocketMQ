using System;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQFactoryConfigurator
    {
        /// <summary>
        /// Configure NameServer Address
        /// </summary>        
        void NameServer(string nameServerAddress);

        /// <summary>
        /// Configure create topic queue num
        /// </summary>        
        void TopicCreate(int topicQueueNum);

        /// <summary>
        /// Configure topic producer
        /// </summary>
        void TopicProducer(string topicName, Action<IRocketMQProducerConfigurator> configure);

        /// <summary>
        /// Subscribe to topic
        /// </summary>
        void TopicConsumer<TContent>(string topicName, string group, Action<IRocketMQConsumerConfigurator<TContent>> configure)
            where TContent : class;
    }
}
