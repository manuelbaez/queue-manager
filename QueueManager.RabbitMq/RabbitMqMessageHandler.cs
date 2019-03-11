using QueueManager.ServerConnector.Abstractions;
using RabbitMQ.Client;
using System.Net.Mime;

namespace QueueManager.RabbitMq
{
    class RabbitMqMessageHandler : IQueueMessageHandler
    {
        private readonly IModel model;
        public ulong MessageDeliveryTag { get; }
        public ContentType ContentType { get; }

        public RabbitMqMessageHandler(IModel model,
            ulong deliveryTag,
            ContentType contentType)
        {
            MessageDeliveryTag = deliveryTag;
            this.model = model;
            ContentType = contentType;
        }

        public void AcknowledgeMessage()
        {
            model.BasicAck(MessageDeliveryTag, false);
        }
    }
}
