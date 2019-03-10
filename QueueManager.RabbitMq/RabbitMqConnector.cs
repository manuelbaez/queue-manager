using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services.Abstraction;
using RabbitMQ.Client;
using System;

namespace QueueManager.RabbitMq
{
    public class RabbitMqConnector<T>: IQueueServerConnnection<T> where T : class, IQueueService
    {
        private IModel channel;
        public RabbitMqConnector(QueueServerConfiguration options)
        {
            var connectionFactory = new ConnectionFactory { Uri = options.Uri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        public void CreateOrAttachQeueue(QueueConfiguration configuration)
        {
            channel.QueueDeclare(queue: configuration.Name,
                exclusive: configuration.Exclusive,
                durable: configuration.Persistent,
                autoDelete: configuration.AutoDelete);
        }

        public void AddQueueListener<S>(string queueName, Action<IQueueMessageHandler, S> handler) where S : class
        {
            channel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: new Consumer<S>(channel, handler)
                );
        }

        public void Dispose()
        {
            channel.Close();
        }
    }
}