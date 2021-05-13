using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewLife.RocketMQ.Bus
{
    public static class TaskFuncExtensions
    {
        private static readonly TaskFactory s_myTaskFactory = new TaskFactory(CancellationToken.None,
            TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        public static TResult RunSync<TResult>(this Func<Task<TResult>> func)
        {
            return s_myTaskFactory.StartNew(func)
                .Unwrap().GetAwaiter().GetResult();
        }

        public static void RunSync(this Func<Task> func)
        {
            s_myTaskFactory.StartNew(func)
              .Unwrap().GetAwaiter().GetResult();
        }
    }
}
