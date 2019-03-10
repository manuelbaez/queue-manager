using QueueManager.ServerConnector.Abstractions;
using System;

namespace QueueManager.Services.Abstraction
{
    public interface IQueueService 
    {
        void AddMessageConsumer<T>(string routingKey, Action<IQueueMessageHandler,T> handler) where T : class;
    }
}
