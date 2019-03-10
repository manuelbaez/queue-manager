using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace QueueManager.Consumer
{
    public class BaseConsumer<T> : IBasicConsumer where T : class
    {
        public IModel Model { get; set; }
        private Action<T> handler;

        public BaseConsumer(IModel channel, Action<T> handler)
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
            var messageBody = Encoding.UTF8.GetString(body);
            handler.Invoke(messageBody as T);
            Model.BasicAck(deliveryTag, false);
        }

        public void HandleModelShutdown(object model, ShutdownEventArgs reason)
        {

        }
    }
}