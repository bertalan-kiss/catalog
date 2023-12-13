using Carting.Infrastructure.Kafka.Configuration;
using Carting.Infrastructure.Kafka.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Carting.Infrastructure.Kafka
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaConsumer<TMessage>(this IServiceCollection collection, IConfiguration configuration) where TMessage : new()
        {
            var kafkaConfiguration = configuration.GetSection("KafkaConfiguration");
            collection.Configure<KafkaConfiguration>(kafkaConfiguration);

            collection.AddSingleton<IKafkaConsumerService, KafkaConsumerService<TMessage>>();
            return collection;
        }
    }
}

