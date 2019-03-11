using QueueManager.Models;
using QueueManager.Services.Abstraction;
using System;
using System.Net.Mime;

namespace QueueManager.ServerConnector.Abstractions
{
    public interface IQueueServerConnnection : IDisposable
    {
        object ConnectionChannel { get; }
        void CreateOrAttachQeueue(QueueConfiguration configuration);
        void AddQueueListener<T>(string queueName, Action<IQueueMessageHandler, T> listener) where T : class;
        void PublishMessage<T>(string destination, string routingKey, T body, ContentType contentType);
    }
    public interface IQueueServerConnnection<S> : IQueueServerConnnection where S : class, IQueueService
    {
    }
}