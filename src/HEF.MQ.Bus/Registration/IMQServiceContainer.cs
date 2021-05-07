using System;

namespace HEF.MQ.Bus
{
    public interface IMQServiceContainer
    {
        void RegisterMessageConsumer<TMessageConsumer>()
            where TMessageConsumer : class, IMessageConsumer;

        void RegisterScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;        

        void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void RegisterScoped<TService>(Func<IMQServiceProvider, TService> factoryMethod)
            where TService : class;

        void RegisterSingleton<TService>(Func<IMQServiceProvider, TService> factoryMethod)
            where TService : class;
    }
}
