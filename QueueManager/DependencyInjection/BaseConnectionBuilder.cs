using Microsoft.Extensions.DependencyInjection;
using QueueManager.DependencyInjection.Abstractions;
using QueueManager.Services.Abstraction;

namespace QueueManager.DependencyInjection
{
    class ConnectionBuilder<T> : IConnectionBuilder<T> where T : class, IQueueService
    {
        public IServiceCollection Services { get; set; }
    }
}
