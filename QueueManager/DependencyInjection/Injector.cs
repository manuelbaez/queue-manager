using Microsoft.Extensions.DependencyInjection;
using QueueManager.Models;
using QueueManager.Services;
using QueueManager.Services.Abstraction;
using System;

namespace QueueManager.DependencyInjection
{
    public static class Injector
    {
        public static void AddQueueServer<T>(this IServiceCollection services, Func<ConfigurationBuilder, QueueServerConfiguration> configurationBuilder) where T : QueueService
        {
            var configuration = configurationBuilder.Invoke(new ConfigurationBuilder());
            var serviceInstance = Activator.CreateInstance(typeof(T), configuration);
            services.AddSingleton<T>(t => serviceInstance as T);
        }
    }
}
