using System;

namespace HEF.MQ.Bus
{
    public interface IMQServiceProvider
    {
        IMQServiceScope CreateScope();

        T GetService<T>() where T : class;

        T GetRequiredService<T>() where T : class;
    }

    public interface IMQServiceScope : IDisposable
    {
        IMQServiceProvider ServiceProvider { get; }
    }
}
