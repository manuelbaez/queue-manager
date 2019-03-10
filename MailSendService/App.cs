using Microsoft.Extensions.Configuration;
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
            server.AddMessageConsumer<object>("main", e => { Console.WriteLine(e);});
        }

        public void Run()
        {
            Console.WriteLine(Configuration.GetSection("AppSettings:Version").Get<string>());
        }
    }
}
