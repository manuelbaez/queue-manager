namespace QueueManager.Models
{
    public class QueueConfiguration
    {
        public string Name { get; set; }
        public bool Exclusive { get; set; }
        public bool Persistent { get; set; }
        public bool AutoDelete { get; set; }
    }
}
