namespace NewLife.RocketMQ.Bus
{
    public class RocketMQClientConfig
    {
        /// <summary>
        /// 名称服务器Address
        /// </summary>
        public string NameServer { get; set; } = "127.0.0.1:9876";

        /// <summary>
        /// Topic队列数量
        /// </summary>
        public int TopicQueueNum { get; set; } = 4;
    }
}
