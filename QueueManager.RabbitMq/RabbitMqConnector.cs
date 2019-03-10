using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using RabbitMQ.Client;
using System;

namespace QueueManager.RabbitMq
{
    public class RabbitMqConnector : IQueueServerConnnector
    {
        private IModel channel;
        public RabbitMqConnector(QueueServerConfiguration options)
        {
            var connectionFactory = new ConnectionFactory { Uri = options.Uri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        public void AddOrAttachQeueue(QueueConfiguration configuration)
        {
            channel.QueueDeclare(queue: configuration.Name,
                exclusive: configuration.Exclusive,
                durable: configuration.Persistent,
                autoDelete: configuration.AutoDelete);
        }

        public void AddQueueListener<T>(string queueName, Action<IQueueMessageHandler, T> handler) where T : class
        {
            channel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: new Consumer<T>(channel, handler)
                );
        }

        public void Dispose()
        {
            channel.Close();
        }
    }
}