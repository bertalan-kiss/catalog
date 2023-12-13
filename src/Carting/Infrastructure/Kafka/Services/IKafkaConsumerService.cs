namespace Carting.Infrastructure.Kafka.Services
{
    public interface IKafkaConsumerService
    {
        Task Consume(CancellationToken cancellationToken);
    }
}

