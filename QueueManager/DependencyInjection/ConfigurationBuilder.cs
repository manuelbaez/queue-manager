using QueueManager.Models;
using System;

namespace QueueManager.DependencyInjection
{
    public class ConfigurationBuilder
    {
        public QueueServerConfiguration UseRabbitMq(QueueServerConnectionParams configuration)
        {
            var options = new QueueServerConfiguration
            {
                Uri = new Uri($"{configuration.Protocol}://{configuration.User}:{configuration.Password}@{configuration.Host}:{configuration.Port}")
            };
            return options;
        }
    }
}
