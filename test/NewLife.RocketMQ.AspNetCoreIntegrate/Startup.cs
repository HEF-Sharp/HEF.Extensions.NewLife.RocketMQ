using HEF.MQ.Bus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewLife.RocketMQ.Bus;

namespace NewLife.RocketMQ.AspNetCoreIntegrate
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMQBus(x =>
            {
                x.AddMessageConsumer<OrderCreateConsumer>();

                x.UsingRocketMQ((registr, cfg) =>
                {
                    cfg.NameServer("172.17.20.6:9876");                   

                    cfg.TopicConsumer("delay_msg_test", "delay_order_create", c =>
                    {
                        c.WithTags("order_create");
                        c.BindTypedMessageConsumer<Order_Create, OrderCreateConsumer>(registr);
                    });
                });
            });

            services.AddMQHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapControllers();
            });
        }
    }
}
