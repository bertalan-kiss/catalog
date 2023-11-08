using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly IValidator<Item> itemValidator;
        private readonly IMessageSenderService messageSenderService;
        private readonly ILogger<ItemService> logger;

        public ItemService(IItemRepository itemRepository, IValidator<Item> itemValidator, IMessageSenderService messageSenderService, ILogger<ItemService> logger)
        {
            this.itemRepository = itemRepository;
            this.itemValidator = itemValidator;
            this.messageSenderService = messageSenderService;
            this.logger = logger;
        }

        public async Task<int> Add(Item item)
        {
            await itemValidator.ValidateAndThrowAsync(item);

            return await itemRepository.Add(item);
        }

        public async Task Delete(int id)
        {
            await itemRepository.Delete(id);
        }

        public async Task<Item> Get(int id)
        {
            return await itemRepository.Get(id);
        }

        public async Task<IEnumerable<Item>> List(int categoryId, int pageSize, int page)
        {
            return await itemRepository.List(categoryId, pageSize, page);
        }

        public async Task Update(Item item)
        {
            await itemValidator.ValidateAndThrowAsync(item);

            await itemRepository.Update(item);

            await PublishUpdateMessage(item);
        }

        private async Task PublishUpdateMessage(Item item)
        {
            try
            {
                await messageSenderService.SendAsync(new Domain.Messages.ItemUpdatedMessage
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    ImageUrl = item.ImageUrl,
                    CategoryId = item.Category.Id,
                    Price = item.Price,
                    Amount = item.Amount
                });
            }
            catch (MessageSendoutFailedException ex)
            {
                logger.Log(LogLevel.Error, ex, ex?.Message);
            }
        }
    }
}

