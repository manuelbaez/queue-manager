using Newtonsoft.Json;
using QueueManager.RabbitMq.Constants;
using QueueManager.ServerConnector.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Xml.Serialization;

namespace QueueManager.RabbitMq
{
    public class Consumer<T> : IBasicConsumer where T : class
    {
        public IModel Model { get; set; }
        private Action<IQueueMessageHandler, T> handler;

        public Consumer(IModel channel, Action<IQueueMessageHandler, T> handler)
        {
            Model = channel;
            this.handler = handler;
        }

        public event EventHandler<ConsumerEventArgs> ConsumerCancelled;

        public void HandleBasicCancel(string consumerTag)
        {

        }

        public void HandleBasicCancelOk(string consumerTag)
        {

        }

        public void HandleBasicConsumeOk(string consumerTag)
        {

        }

        public void HandleBasicDeliver(string consumerTag,
            ulong deliveryTag, bool redelivered,
            string exchange, string routingKey,
            IBasicProperties properties, byte[] body)
        {
            var contentType = new ContentType((properties.ContentType is null) ? "text/plain" : properties.ContentType);
            var messageString = Encoding.UTF8.GetString(body);
            object messageBody;
            switch (contentType.Name)
            {
                case MimeTypes.Json:
                    messageBody = JsonConvert.DeserializeObject<T>(messageString);
                    break;
                case MimeTypes.XML:
                    var deserializer = new XmlSerializer(typeof(T));
                    var messageStream = new MemoryStream(body);
                    messageBody = deserializer.Deserialize(messageStream);
                    break;
                default:
                    messageBody = messageString;
                    break;
            }

            handler.Invoke(new RabbitMqMessageHandler(Model, deliveryTag, contentType), messageBody as T);
        }

        public void HandleModelShutdown(object model, ShutdownEventArgs reason)
        {

        }
    }
}