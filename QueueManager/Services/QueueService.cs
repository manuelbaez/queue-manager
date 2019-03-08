using QueueManager.Models;
using RabbitMQ.Client;

namespace QueueManager.Services
{
    public abstract class QueueService
    {
        public QueueService(QueueServerConfiguration options)
        {
            var connectionFactory = new ConnectionFactory { Uri = options.Uri };
        }
    }
}
