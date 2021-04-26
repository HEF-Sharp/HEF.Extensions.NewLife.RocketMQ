using System;

namespace NewLife.RocketMQ.Bus
{
    public class RocketMQClientConfig
    {
        /// <summary>
        /// 名称服务器Address
        /// </summary>
        public String NameServer { get; set; }

        /// <summary>
        /// Topic队列数量
        /// </summary>
        public int TopicQueueNum { get; set; }
    }
}
