using System;
using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using RabbitMQ.Client;

namespace QueueManager.ServerConnector
{
    public class RabbitMqConnector : IQueueServerConnnection
    {
        private IModel channel;
        public RabbitMqConnector(QueueServerConfiguration options)
        {
            var connectionFactory = new ConnectionFactory { Uri = options.Uri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }
        
        public void Dispose()
        {
            channel.Close();
        }
    }
}