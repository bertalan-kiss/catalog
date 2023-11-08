namespace Catalog.Domain.Configuration
{
    public class KafkaConfiguration
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
        public int MessageTimeoutMs { get; set; }
    }
}
