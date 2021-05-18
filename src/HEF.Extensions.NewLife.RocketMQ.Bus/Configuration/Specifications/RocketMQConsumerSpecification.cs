using HEF.MQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQConsumerSpecification
    {
        string TopicName { get; }

        /// <summary>
        /// 消费组
        /// </summary>
        string Group { get; }

        /// <summary>
        /// 关注标签列表
        /// </summary>
        public string[] Tags { get; }

        Action<ConsumerContainer> CreateAddTopicConsumerAction();
    }

    public class RocketMQConsumerSpecification
        : IRocketMQConsumerSpecification, IRocketMQConsumerConfigurator        
    {
        private Action<Consumer> _consumerConfigure;

        private List<string> _subscribeTags = new();
        private IMQMessageDeserializer<MessageExt> _messageDeserializer;

        private Func<IMQMessageDeserializer<MessageExt>, IMQMessageConsumeExecutor<MessageExt>> _consumeExecutorFactory;

        public RocketMQConsumerSpecification(string topicName, string group)
        {
            TopicName = topicName;
            Group = group;

            _messageDeserializer = new RocketMQMessageJsonDeserializer();
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

        public void Consume(Func<IMQMessageConsumeExecutor<MessageExt>> executorFactory)
        {
            if (executorFactory == null)
                throw new ArgumentNullException(nameof(executorFactory));

            _consumeExecutorFactory = deserializer => executorFactory.Invoke();
        }

        public void TypedConsume(Func<IMQMessageDeserializer<MessageExt>, IMQMessageConsumeExecutor<MessageExt>> executorFactory)
        {
            _consumeExecutorFactory = executorFactory;
        }

        public Action<ConsumerContainer> CreateAddTopicConsumerAction()
        {
            var consumeFunction = CreateConsumeFunction();

            return consumeContainer =>
            {
                consumeContainer.AddTopicConsumer(Group, TopicName, _consumerConfigure, consumeFunction, Tags);
            };
        }

        #region Create ConsumeFunction
        private Func<MessageQueue, MessageExt[], bool> CreateConsumeFunction()
        {
            if (_consumeExecutorFactory == null)
                throw new InvalidOperationException("not configured topic message consume executor");

            return (queue, messageArr) =>
            {
                using var consumeExecutor = _consumeExecutorFactory.Invoke(_messageDeserializer);

                foreach (var message in messageArr)
                {
                    Func<Task<bool>> executeFunc = () => consumeExecutor.Execute(message);

                    var result = executeFunc.RunSync();

                    if (!result) return false;
                }

                return true;
            };
        }
        #endregion
    }
}
