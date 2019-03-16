using Microsoft.Extensions.DependencyInjection;
using QueueManager.DependencyInjection.Abstractions;
using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services.Abstraction;
using System;

namespace QueueManager.RabbitMq.Extensions.DependencyInjection
{
    public static class ConnectionBuilderExtensions
    {
        public static void UseRabbitMq<T>(this IConnectionBuilder<T> configurationBuilder,
            QueueServerConnectionParams configuration, int degreeOfParallelism = 1)
            where T : class, IQueueService
        {
            var options = new QueueServerConfiguration
            {
                Uri = new Uri($"{configuration.Protocol}://{configuration.User}:{configuration.Password}@{configuration.Host}:{configuration.Port}"),
                DegreeOfParallelism = degreeOfParallelism
            };
            var connection = new RabbitMqConnector<T>(options);
            configurationBuilder.Services.AddSingleton<IQueueServerConnnection<T>> (_ => connection);
        }
    }
}
