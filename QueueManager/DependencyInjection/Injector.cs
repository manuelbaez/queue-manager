using Microsoft.Extensions.DependencyInjection;
using QueueManager.DependencyInjection.Abstractions;
using QueueManager.Services.Abstraction;
using System;

namespace QueueManager.DependencyInjection
{
    public static class Injector
    {
        public static void AddQueueServer<T>(this IServiceCollection services, Action<IConnectionBuilder<T>> connectionBuilder)
        where T : class, IQueueService
        {
            connectionBuilder.Invoke(new ConnectionBuilder<T> { Services = services });
            services.AddSingleton<T>();
        }
    }
}
