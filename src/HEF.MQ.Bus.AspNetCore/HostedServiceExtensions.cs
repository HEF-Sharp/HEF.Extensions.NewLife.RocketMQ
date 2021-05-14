using Microsoft.Extensions.DependencyInjection;

namespace HEF.MQ.Bus
{
    public static class HostedServiceExtensions
    {
        public static IServiceCollection AddMQHostedService(this IServiceCollection services)
        {
            return services.AddHostedService(provider =>
            {
                var busControl = provider.GetRequiredService<IMQBusControl>();

                return new MQHostedService(busControl);
            });
        }
    }
}
