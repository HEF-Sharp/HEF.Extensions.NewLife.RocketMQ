using HEF.MQ.Bus;
using Microsoft.Extensions.Logging;
using NewLife.RocketMQ.Protocol;
using System.Threading.Tasks;
using System;

namespace NewLife.RocketMQ.AspNetCoreIntegrate
{
    public class OrderCreateConsumer : IMQTypedMessageConsumer<MessageExt, Order_Create>
    {
        private readonly ILogger<OrderCreateConsumer> _logger;

        public OrderCreateConsumer(ILogger<OrderCreateConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Consume(MQTypedMessage<MessageExt, Order_Create> typedMessage)
        {
            await Task.Yield();

            _logger.LogInformation($"接收下单消息，接收时间：{DateTime.UtcNow}, 订单号：{typedMessage.Content.OrderNo}, 下单时间：{typedMessage.Content.CreateTime}");

            return true;
        }
    }
}
