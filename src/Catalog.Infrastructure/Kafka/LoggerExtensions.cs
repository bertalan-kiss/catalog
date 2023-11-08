using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Kafka
{
    internal static class LoggerExtensions
    {
        public static void LogProducerError(this ILogger logger, Error kafkaError) =>
            logger.LogError("Kafka producer error: {@ex}", kafkaError);
    }
}
