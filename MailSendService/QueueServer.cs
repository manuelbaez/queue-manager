using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services;

namespace SendMail
{
    public class QueueServer : QueueService
    {
        public QueueServer(IQueueServerConnnection<QueueServer> connnection) : base(connnection)
        {
        }
    }
}
