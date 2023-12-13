using Carting.Infrastructure.Kafka.Services;

namespace Carting.Api.HostedService
{
    public class CartingHostedService : BackgroundService
	{
        private readonly IEnumerable<IKafkaConsumerService> kafkaConsumerServices;
        private readonly ILogger<CartingHostedService> logger;

        public CartingHostedService(IEnumerable<IKafkaConsumerService> kafkaConsumerServices, ILogger<CartingHostedService> logger)
        {
            this.kafkaConsumerServices = kafkaConsumerServices;
            this.logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerTasks = kafkaConsumerServices.Select(consumer =>
            {
                logger.LogInformation($"Starting Kafka consumer {consumer.GetType().FullName}...");
                return Task.Run(async () =>
                {
                    try
                    {
                        await consumer.Consume(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Consumer {ConsumerType} failed.", consumer.GetType().Name);
                    }
                }, stoppingToken);
            });

            return Task.WhenAll(consumerTasks);
        }
    }
}

