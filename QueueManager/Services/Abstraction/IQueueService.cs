using System;

namespace QueueManager.Services.Abstraction
{
    public interface IQueueService : IDisposable
    {
        void AddMessageConsumer<T>(string routingKey, Action<T> handler) where T : class;
    }
}
