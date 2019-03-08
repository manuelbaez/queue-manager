using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueueManager.DependencyInjection;
using System;

namespace SendMail
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public App App { get; }

        public Startup(IConfiguration configuration, App app)
        {
            Configuration = configuration;
            App = app;
        }

        public static void ConfigureServices(IServiceCollection services,IConfiguration configuration)
        {
            services.AddQueueServer<QueueServer>(t => t.UseRabbitMq(configuration.GetQueueServerConnectionParams("local")));
           
        }

        public void Start() {
            App.Run();
            Console.ReadKey();
        }

    }
}
