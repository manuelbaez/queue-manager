using Newtonsoft.Json;
using QueueManager.RabbitMq.Constants;
using QueueManager.RabbitMq.Exceptions;
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
            if (properties.ContentType is null)
            {
                throw new NoContentTypeException();
            }
            var contentType = new ContentType(properties.ContentType);
            var messageString = Encoding.UTF8.GetString(body);
            object messageBody;
            switch (contentType.MediaType)
            {
                case MimeTypes.Json:
                    messageBody = JsonConvert.DeserializeObject<T>(messageString);
                    break;
                case MimeTypes.XML:
                    var deserializer = new XmlSerializer(typeof(T));
                    var messageStream = new MemoryStream(body);
                    messageBody = deserializer.Deserialize(messageStream);
                    break;
                case MimeTypes.PlainText:
                    messageBody = messageString;
                    break;
                default:
                    throw new InvalidContentTypeException();
            }

            handler.Invoke(new RabbitMqMessageHandler(Model, deliveryTag, contentType), messageBody as T);
        }

        public void HandleModelShutdown(object model, ShutdownEventArgs reason)
        {

        }
    }
}