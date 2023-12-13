using Carting.Infrastructure.DataAccess.Repositories;
using Carting.Infrastructure.Kafka.Messages;
using Carting.Infrastructure.Kafka.Services;
using Microsoft.Extensions.Logging;

namespace Carting.Core.Services
{
    public class ItemUpdatedMessageProcessor : IMessageProcessor<ItemUpdatedMessage>
    {
        private readonly ICartingRepository repository;
        private readonly ILogger<ItemUpdatedMessageProcessor> logger;

        public ItemUpdatedMessageProcessor(ICartingRepository repository, ILogger<ItemUpdatedMessageProcessor> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task ProcessMessageAsync(ItemUpdatedMessage message)
        {
            var item = new Infrastructure.DataAccess.Models.CartItem
            {
                _id = message.Id,
                ExternalId = message.Identifier,
                Name = message.Name,
                Image = new Infrastructure.DataAccess.Models.Image
                {
                    Url = message.ImageUrl
                },
                Price = message.Price,
                Quantity = message.Amount
            };

            if (!string.IsNullOrEmpty(message.ImageUrl))
            {
                item.Image = new Infrastructure.DataAccess.Models.Image
                {
                    Url = message.ImageUrl
                };
            }

            try
            {
                var result = await Task.Run(() => repository.UpdateCartItem(item));

                if (!result)
                {
                    logger.LogWarning($"Could not process update, cart item not found with id: {message.Id}");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
