using QueueManager.Models;
using QueueManager.Services.Abstraction;
using System;

namespace QueueManager.ServerConnector.Abstractions
{
    public interface IQueueServerConnnection : IDisposable
    {
        void CreateOrAttachQeueue(QueueConfiguration configuration);
        void AddQueueListener<T>(string queueName, Action<IQueueMessageHandler, T> listener) where T : class;
    }
    public interface IQueueServerConnnection<S> : IQueueServerConnnection where S : class, IQueueService
    {
    }
}