namespace Carting.Infrastructure.Kafka.Configuration
{
    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
        public string GroupId { get; set; }
        public int SessionTimeoutMs { get; set; }
    }
}

