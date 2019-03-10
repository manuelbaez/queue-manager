using QueueManager.ServerConnector.Abstractions;
using System;

namespace QueueManager.Services.Abstraction
{
    public interface IQueueService
    {
        void AddMessageConsumer<S>(string routingKey, Action<IQueueMessageHandler,S> handler) where S : class;
    }
}
