namespace NewLife.RocketMQ.Bus
{
    public interface IRocketMQConsumerSpecification : IRocketMQProducerSpecification
    {
        /// <summary>
        /// 消费组
        /// </summary>
        string Group { get; }
    }
}
