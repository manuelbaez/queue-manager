using Microsoft.Extensions.DependencyInjection;
using QueueManager.DependencyInjection.Abstractions;
using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services;
using QueueManager.Services.Abstraction;
using System;

namespace QueueManager.DependencyInjection
{
    public static class Injector
    {
        private class BaseConfigurationBuilder : IConnectionBuilder { };
        public static void AddQueueServer<T>(this IServiceCollection services, Func<IConnectionBuilder, IQueueServerConnnector> configurationBuilder) where T : QueueService
        {
            var configuration = configurationBuilder.Invoke(new BaseConfigurationBuilder());
            var serviceInstance = Activator.CreateInstance(typeof(T), configuration);
            services.AddSingleton<T>(t => serviceInstance as T);
        }
    }
}
