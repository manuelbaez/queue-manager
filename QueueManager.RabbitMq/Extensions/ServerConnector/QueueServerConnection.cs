using QueueManager.RabbitMq.Models;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services.Abstraction;
using RabbitMQ.Client;
using System.Net.Mime;

namespace QueueManager.RabbitMq.Extensions.ServerConnector
{
    public static class QueueServerConnection
    {
        public static void CreateOrAttachExchange(this IQueueServerConnnection queueServerConnnection, ExchangeConfiguration configuration)
        {
            var channel = queueServerConnnection.ConnectionChannel as IModel;
            channel.ExchangeDeclare(exchange: configuration.Name, type: configuration.Type,
                autoDelete: configuration.AutoDelete, durable: configuration.Persistent);
        }

        public static void BindQueueToExchange(this IQueueServerConnnection queueServerConnnection, string exchangeName, string queueName, string routingKey)
        {
            var channel = queueServerConnnection.ConnectionChannel as IModel;
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey);
        }

        public static void BindQueueToExchange(this IQueueService queueService, string exchangeName, string queueName, string routingKey)
        {
            queueService.Connection.BindQueueToExchange(exchangeName, queueName, routingKey);
        }

        public static void ConfigureExchange(this IQueueService queueService, ExchangeConfiguration configuration)
        {
            queueService.Connection.CreateOrAttachExchange(configuration);
        }

    }
}
