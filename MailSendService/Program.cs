using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace SendMail
{
    class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            
            var serviceCollection = new ServiceCollection();
            Startup.ConfigureServices(serviceCollection, configuration);
            serviceCollection.AddTransient<App>();
            serviceCollection.AddTransient<Startup>();

            serviceCollection.AddTransient<IConfiguration>(t => {
                return configuration;
            });

            var serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<Startup>().Start();
        }
    }
}
