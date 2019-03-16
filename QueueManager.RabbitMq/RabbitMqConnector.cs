using Newtonsoft.Json;
using QueueManager.Models;
using QueueManager.RabbitMq.Constants;
using QueueManager.RabbitMq.Exceptions;
using QueueManager.ServerConnector.Abstractions;
using QueueManager.Services.Abstraction;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace QueueManager.RabbitMq
{
    public class RabbitMqConnector<T> : IQueueServerConnnection<T> where T : class, IQueueService
    {
        private IModel channel;

        public object ConnectionChannel => channel;

        public RabbitMqConnector(QueueServerConfiguration options)
        {
            var connectionFactory = new ConnectionFactory { Uri = options.Uri };
            var connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        public void CreateOrAttachQeueue(QueueConfiguration configuration)
        {
            channel.QueueDeclare(queue: configuration.Name,
                exclusive: configuration.Exclusive,
                durable: configuration.Persistent,
                autoDelete: configuration.AutoDelete);
        }

        public void AddQueueListener<S>(string queueName, Action<IQueueMessageHandler, S> handler) where S : class
        {
            channel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: new Consumer<S>(channel, handler)
                );
        }

        public void PublishMessage<S>(string destination, string routingKey, S body, ContentType contentType)
        {
            string serializedMessage;
            switch (contentType.MediaType)
            {
                case MimeTypes.Json:
                    serializedMessage = JsonConvert.SerializeObject(body);
                    break;
                case MimeTypes.PlainText:
                    if (body is string)
                    {
                        serializedMessage = body as string;
                    }
                    else
                    {
                        throw new InvalidMessageBodyException();
                    }
                    break;
                case MimeTypes.XML:
                    var serializer = new XmlSerializer(typeof(S));
                    using (var stringWriter = new StringWriter())
                    {
                        using (var xmlWriter= XmlWriter.Create(stringWriter))
                        {
                            serializer.Serialize(xmlWriter, body);
                            serializedMessage = stringWriter.ToString();
                        }
                    }
                    break;
                default:
                    throw new InvalidContentTypeException();
            }
            var messageProperties = channel.CreateBasicProperties();
            messageProperties.ContentType = contentType.MediaType;
            var messageBody = Encoding.UTF8.GetBytes(serializedMessage);
            channel.BasicPublish(
                exchange: destination,
                routingKey: routingKey,
                basicProperties: messageProperties,
                body:messageBody
                );
        }

        public void Dispose()
        {
            channel.Close();
        }

    }
}