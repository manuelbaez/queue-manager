using QueueManager.Models;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services;

namespace SendMail
{
    public class QueueServer : QueueService
    {
        public QueueServer(IQueueServerConnnector configuration) : base(configuration)
        {
        }
    }
}
