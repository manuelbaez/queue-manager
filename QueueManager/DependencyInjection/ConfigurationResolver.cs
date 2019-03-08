using Microsoft.Extensions.Configuration;
using QueueManager.Models;

namespace QueueManager.DependencyInjection
{
    public static class ConfigurationResolver
    {
        public static QueueServerConnectionParams GetQueueServerConnectionParams(this IConfiguration configuration, string connectionName)
        {
            return configuration.GetSection($"QueueConnections:{connectionName}").Get<QueueServerConnectionParams>();
        }
    }
}
