using QueueManager.Models;
using System;

namespace QueueManager.ServerConnector.Abstractions
{
    public interface IQueueServerConnnector : IDisposable
    {
        void AddOrAttachQeueue(QueueConfiguration configuration);
        void AddQueueListener<T>(string queueName, Action<IQueueMessageHandler,T> listener) where T : class;
    }
}