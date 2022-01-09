using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoundersPC.SharedKernel.Helpers;

public static class TaskHelper
{
    private static readonly TaskFactory _taskFactory = 
        new(CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

    public static void RunSync(Func<Task> task) =>
        _taskFactory
            .StartNew(task)
            .Unwrap()
            .GetAwaiter()
            .GetResult();

    public static TResult RunSync<TResult>(Func<Task<TResult>> task) =>
        _taskFactory
            .StartNew(task)
            .WaitAsync(new TimeSpan(0, 0, 30))
            .Unwrap()
            .GetAwaiter()
            .GetResult();
}