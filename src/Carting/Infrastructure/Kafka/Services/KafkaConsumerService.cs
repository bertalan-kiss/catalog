using Carting.Infrastructure.Kafka.Configuration;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Carting.Infrastructure.Kafka.Services
{
    public class KafkaConsumerService<TMessage> : IKafkaConsumerService where TMessage : new()
    {
        private readonly ILogger<KafkaConsumerService<TMessage>> logger;
        private readonly IMessageProcessor<TMessage> processor;
        private readonly Lazy<IConsumer<Ignore, TMessage>> consumerLazy;
        private IConsumer<Ignore, TMessage> Consumer => consumerLazy.Value;

        public KafkaConsumerService(IOptions<KafkaConfiguration> kafkaConfiguration, ILogger<KafkaConsumerService<TMessage>> logger, IMessageProcessor<TMessage> processor)
        {
            consumerLazy = new Lazy<IConsumer<Ignore, TMessage>>(CreateConsumer(kafkaConfiguration.Value));
            this.logger = logger;
            this.processor = processor;
        }

        public async Task Consume(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = Consumer.Consume(cancellationToken);
                        await TryProcessResultAsync(consumeResult);
                    }
                    catch (KafkaException ex)
                    {
                        LogException(ex);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation($"Consumer '{GetConsumerName()}' cancelled");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
            finally
            {
                if (consumerLazy.IsValueCreated)
                {
                    Consumer.Close();
                }
            }
        }

        private string GetConsumerName() => typeof(TMessage).Name;

        private IConsumer<Ignore, TMessage> CreateConsumer(KafkaConfiguration kafkaConfiguration)
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = kafkaConfiguration.BootstrapServers,
                //SessionTimeoutMs = kafkaConfiguration.SessionTimeoutMs,
                GroupId = kafkaConfiguration.GroupId,
                EnableAutoCommit = true
            };

            var consumerBuilder = new ConsumerBuilder<Ignore, TMessage>(config)
                .SetValueDeserializer(new KafkaMessageDeserializer<TMessage>())
                .SetErrorHandler(ErrorHandler);

            var consumer = consumerBuilder.Build();

            consumer.Subscribe(kafkaConfiguration.Topic);

            return consumer;
        }

        private async Task TryProcessResultAsync(ConsumeResult<Ignore, TMessage> consumeResult)
        {
            TMessage message = default;

            try
            {
                var kafkaMessage = consumeResult.Message;
                message = kafkaMessage.Value;

                await processor.ProcessMessageAsync(message);
            }
            catch (Exception ex)
            {
                logger.LogError($"Consumer '{GetConsumerName()}' consumed message: {@message}. Exception: {@ex}", message, ex);
            }
        }

        private void ErrorHandler(IConsumer<Ignore, TMessage> consumer, Error error) =>
            logger.LogError($"Internal consumer error in consumer '{GetConsumerName()}': {@error}", error);

        private void LogException(Exception ex) => logger.LogError($"Unexpected Kafka consuming error in consumer '{GetConsumerName()}': {@ex}", ex);

    }
}

