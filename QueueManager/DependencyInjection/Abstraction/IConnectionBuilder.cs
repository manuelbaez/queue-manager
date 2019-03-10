using Microsoft.Extensions.DependencyInjection;
using QueueManager.Services.Abstraction;

namespace QueueManager.DependencyInjection.Abstractions
{
    public interface IConnectionBuilder<T> where T : class, IQueueService
    {
        IServiceCollection Services { get; set; }
    }
}
