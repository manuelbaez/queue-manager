using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using System;
using System.Net.Mime;

namespace QueueManager.Services.Abstraction
{
    public interface IQueueService
    {
        IQueueServerConnnection Connection { get; }
        void ConfigureQueue(QueueConfiguration configuration);
        void AddMessageConsumer<S>(string queueName, Action<IQueueMessageHandler, S> handler) where S : class;
        void PublishMessage<T>(string destination, string routingKey, T body, ContentType contentType);
    }
}
