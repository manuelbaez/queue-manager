using QueueManager.Models;
using QueueManager.Services;

namespace SendMail
{
    public class QueueServer : QueueService
    {
        public QueueServer(QueueServerConfiguration configuration) : base(configuration)
        {
        }
    }
}
