using QueueManager.DependencyInjection.Abstractions;
using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using System;

namespace QueueManager.RabbitMq.DependencyInjection
{
    public static class ConnectionBuilderExtensions
    {
        public static IQueueServerConnnector UseRabbitMq(this IConnectionBuilder configurationBuilder, QueueServerConnectionParams configuration, int degreeOfParallelism = 1)
        {
            var options = new QueueServerConfiguration
            {
                Uri = new Uri($"{configuration.Protocol}://{configuration.User}:{configuration.Password}@{configuration.Host}:{configuration.Port}"),
                DegreeOfParallelism = degreeOfParallelism
            };
            return new RabbitMqConnector(options);
        }
    }
}
