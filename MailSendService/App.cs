using Microsoft.Extensions.Configuration;
using QueueManager.Models;
using QueueManager.Services.Abstraction;
using System;
using QueueManager.RabbitMq.Extensions.ServerConnector;
using QueueManager.RabbitMq.Models;
using RabbitMQ.Client;
using System.Net.Mime;
using QueueManager.RabbitMq.Constants;

namespace SendMail
{
    public class App
    {
        private readonly IQueueService server;
        public IConfiguration Configuration { get; }
        public App(IConfiguration configuration, QueueServer server)
        {
            Configuration = configuration;
            this.server = server;

        }

        public void Run()
        {
            Console.WriteLine(Configuration.GetSection("AppSettings:Version").Get<string>());
            server.ConfigureExchange(new ExchangeConfiguration { Name = "client.mails", AutoDelete = false, Persistent = true, Type = ExchangeType.Topic });
            server.ConfigureQueue(new QueueConfiguration { Name = "emails", AutoDelete = false, Persistent = true, Exclusive = false });
            server.BindQueueToExchange("client.mails", "emails", "#.email.#");

            server.AddMessageConsumer<string>("emails", (handler, message) =>
            {
                try
                {
                    Console.WriteLine(message);
                    handler.AcknowledgeMessage();
                }
                catch (Exception e)
                {
                    
                }
            });

            server.PublishMessage("client.mails", "fast.email.client", "hello", new ContentType(MimeTypes.PlainText));

        }
    }
}
