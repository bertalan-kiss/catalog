namespace Carting.Infrastructure.Kafka.Services
{
    public interface IMessageProcessor<TMessage>
    {
        Task ProcessMessageAsync(TMessage message);
    }
}

