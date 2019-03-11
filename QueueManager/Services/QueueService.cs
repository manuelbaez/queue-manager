using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace QueueManager.Services
{
    public abstract class QueueService : IQueueService
    {
        public IQueueServerConnnection Connection { get; }
        public Dictionary<string, Func<object>> Queues { get; set; }

        public QueueService(IQueueServerConnnection connection)
        {
            Connection = connection;
        }
        public void ConfigureQueue(QueueConfiguration configuration)
        {
            Connection.CreateOrAttachQeueue(configuration);
        }
        public void AddMessageConsumer<T>(string queueName, Action<IQueueMessageHandler, T> handler) where T : class
        {
            Connection.AddQueueListener(queueName, handler);
        }

        public void PublishMessage<T>(string destination, string routingKey, T body, ContentType contentType)
        {
            Connection.PublishMessage<T>(destination, routingKey, body, contentType);
        }
    }
}
