namespace QueueManager.RabbitMq.Models
{
    public class ExchangeConfiguration
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Persistent { get; set; }
        public bool AutoDelete { get; set; }
    }
}
