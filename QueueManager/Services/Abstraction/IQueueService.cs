using System;

namespace QueueManager.Services.Abstraction
{
    public interface IQueueService
    {
        void AddMessageListener<T>(string routingKey, Action<T> handler);
    }
}
