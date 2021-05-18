using System;

namespace NewLife.RocketMQ.AspNetCoreIntegrate
{
    public class Order_Create
    {
        public string OrderNo { get; set; }

        public string ProductId { get; set; }

        public int Num { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
