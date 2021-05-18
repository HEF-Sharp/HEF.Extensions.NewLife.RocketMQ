using Microsoft.AspNetCore.Mvc;
using NewLife.RocketMQ.Bus;
using NewLife.RocketMQ.Protocol;
using System;

namespace NewLife.RocketMQ.AspNetCoreIntegrate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class messageController : ControllerBase
    {
        private readonly IRocketMQProducerProvider _producerProvider;        

        public messageController(IRocketMQProducerProvider producerProvider)
        {
            _producerProvider = producerProvider ?? throw new ArgumentNullException(nameof(producerProvider));
        }

        [HttpPost("order/create/publish")]
        public string PublishDelayOrderCreate()
        {
            var orderCreate = new Order_Create
            {
                OrderNo = $"O{DateTime.UtcNow:yyyyMMddHHmmss}{DateTime.UtcNow.Ticks.ToString().ToCharArray()[^5..^0].AsSpan().ToString()}",
                ProductId = Guid.NewGuid().ToString("N"),
                Num = Convert.ToInt32(DateTime.UtcNow.Ticks % 5) + 1,
                CreateTime = DateTime.UtcNow
            };

            var topicTypedProducer = _producerProvider.GetTypedProducer("delay_msg_test");

            var result = topicTypedProducer.Publish(orderCreate, b => b.Tag("order_create").Delay(2));

            if (result.Status == SendStatus.SendOK)
                return $"下单消息发送成功, 订单号：{orderCreate.OrderNo}";

            return $"下单消息发送失败";
        }
    }
}
