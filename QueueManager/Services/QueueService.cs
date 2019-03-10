using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services.Abstraction;
using System;
using System.Collections.Generic;

namespace QueueManager.Services
{
    public abstract class QueueService:IQueueService
    {
        private readonly IQueueServerConnnection connnection;

        public Dictionary<string, Func<object>> Queues { get; set; }
        public QueueService(IQueueServerConnnection connnection)
        {
            this.connnection = connnection;
        }
        public void ConfigureQueue(QueueConfiguration configuration)
        {
            connnection.CreateOrAttachQeueue(configuration);
        }
        public void AddMessageConsumer<T>(string queueName, Action<IQueueMessageHandler, T> handler) where T : class
        {
            connnection.AddQueueListener(queueName, handler);
        }
    }
}
