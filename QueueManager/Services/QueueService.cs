using QueueManager.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace QueueManager.Services
{
    public abstract class QueueService : IDisposable
    {
        private IModel channel;
        public Dictionary<string, Func<object>> Queues { get; set; }
        public QueueService(QueueServerConfiguration options)
        {
            var connectionFactory = new ConnectionFactory { Uri = options.Uri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        public void AddMessageListener<T>(string routingKey, Action<T> handler) 
        {
            channel.QueueDeclare(
                queue:routingKey);
            channel.BasicConsume(
                queue: routingKey,
                autoAck: false,
                consumer: null
                );
        }

        public void Dispose()
        {
            channel.Close();
        }
    }
}
