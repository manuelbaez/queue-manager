using Microsoft.Extensions.Configuration;
using QueueManager.Models;
using System;

namespace SendMail
{
    public class App
    {
        private readonly QueueServer server;
        public IConfiguration Configuration { get; }
        public App(IConfiguration configuration, QueueServer server)
        {
            Configuration = configuration;
            this.server = server;

        }

        public void Run()
        {
            Console.WriteLine(Configuration.GetSection("AppSettings:Version").Get<string>());
            server.ConfigureQueue(new QueueConfiguration { Name = "emails", AutoDelete = false, Persistent = true, Exclusive = false });
            server.AddMessageConsumer<string>("emails", (handler, message) =>
            {
                Console.WriteLine(message);
                handler.AcknowledgeMessage();
            });
        }
    }
}
