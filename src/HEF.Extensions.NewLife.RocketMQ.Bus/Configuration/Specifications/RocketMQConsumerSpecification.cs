using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQConsumerSpecification : IRocketMQProducerSpecification
    {
        /// <summary>
        /// 消费组
        /// </summary>
        string Group { get; }

        /// <summary>
        /// 关注标签列表
        /// </summary>
        public string[] Tags { get; }
    }

    public class RocketMQConsumerSpecification<TContent>
        : IRocketMQConsumerSpecification, IRocketMQConsumerConfigurator<TContent>
        where TContent : class
    {
        private Action<Consumer> _consumerConfigure;

        private List<string> _subscribeTags = new();
        private IMQMessageDeserializer<MessageExt> _messageDeserializer;        

        public RocketMQConsumerSpecification(string topicName, string group)
        {
            TopicName = topicName;
            Group = group;
        }

        public string TopicName { get; }

        public string Group { get; }

        public string[] Tags => _subscribeTags.ToArray();        

        public void WithTags(params string[] tags)
        {
            if (tags != null && tags.Length > 0)
            {
                _subscribeTags.AddRange(tags.Distinct());
            }
        }

        public void Deserialize(IMQMessageDeserializer<MessageExt> deserializer)
        {
            _messageDeserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));
        }

        public void Configure(Action<Consumer> configure)
        {
            _consumerConfigure = configure;
        }
    }
}
