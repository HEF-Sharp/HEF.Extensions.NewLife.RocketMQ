using HEF.MQ.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace NewLife.RocketMQ.AspNetCore
{
    public static class HostedServiceExtensions
    {
        public static IServiceCollection AddRocketMQHostedService(this IServiceCollection services)
        {
            return services.AddHostedService(provider =>
            {
                var rocketMQControl = provider.GetRequiredService<IMQBusControl>();

                return new RocketMQHostedService(rocketMQControl);
            });
        }
    }
}
