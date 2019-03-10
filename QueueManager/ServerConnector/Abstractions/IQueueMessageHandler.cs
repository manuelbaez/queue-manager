using System.Net.Mime;

namespace QueueManager.ServerConnector.Abstractions
{
    public interface IQueueMessageHandler
    {
        ulong MessageDeliveryTag { get; }
        ContentType ContentType { get; }
        void AcknowledgeMessage();
    }
}
