using Catalog.Application.Interfaces;
using Catalog.Domain.Configuration;
using Catalog.Domain.Exceptions;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Catalog.Infrastructure.Kafka
{
    public class MessageSenderService : IMessageSenderService
    {
        private readonly string Topic;
        private readonly IProducer<Null, string> producer;
        private readonly ILogger<MessageSenderService> logger;

        public MessageSenderService(ILogger<MessageSenderService> logger, IOptions<KafkaConfiguration> kafkaConfiguration)
        {
            this.logger = logger;

            Topic = kafkaConfiguration.Value.Topic;
            producer = CreateProducerBuilder(kafkaConfiguration.Value).Build();
        }

        public async Task SendAsync<T>(T message)
        {
            var deliveryResult = await producer.ProduceAsync(
                Topic, 
                new Message<Null, string> 
                { 
                    Value = JsonConvert.SerializeObject(message) 
                });

            if (deliveryResult.Status != PersistenceStatus.Persisted)
            {
                throw new MessageSendoutFailedException($"Message {message} was not sent. Kafka delivery status: {deliveryResult.Status}.");
            }
        }

        private ProducerBuilder<Null, string> CreateProducerBuilder(KafkaConfiguration configuration)
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = configuration.BootstrapServers,
                MessageTimeoutMs = configuration.MessageTimeoutMs,
                Acks = Acks.Leader
            };

            return new ProducerBuilder<Null, string>(config).SetErrorHandler(ErrorHandler);
        }

        private void ErrorHandler(IProducer<Null, string> consumer, Error error) => logger.LogProducerError(error);
    }
}
