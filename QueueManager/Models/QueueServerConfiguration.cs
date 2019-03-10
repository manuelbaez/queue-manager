using QueueManager.ServerConnector.Abstractions;
using System;

namespace QueueManager.Models
{
    public class QueueServerConfiguration
    {
        public Uri Uri { get; set; }
        public int DegreeOfParallelism { get; set; } = 1;
    }
}
