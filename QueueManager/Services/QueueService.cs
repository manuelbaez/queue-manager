using QueueManager.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using QueueManager.Services.Abstraction;
using QueueManager.Consumer;

namespace QueueManager.Services
{
    public abstract class QueueService : IQueueService
    {
      
        public Dictionary<string, Func<object>> Queues { get; set; }
        public QueueService()
        {
           
        }

        public void AddMessageConsumer<T>(string routingKey, Action<T> handler) where T : class
        {
            channel.QueueDeclare(
                queue: routingKey);
            channel.BasicConsume(
                queue: routingKey,
                autoAck: false,
                consumer: new BaseConsumer<T>(channel, handler)
                );
        }

        public void Dispose()
        {
            channel.Close();
        }
    }
}
