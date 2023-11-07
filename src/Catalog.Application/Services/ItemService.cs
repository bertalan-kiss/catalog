using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using FluentValidation;

namespace Catalog.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly IValidator<Item> itemValidator;

        public ItemService(IItemRepository itemRepository, IValidator<Item> itemValidator)
        {
            this.itemRepository = itemRepository;
            this.itemValidator = itemValidator;
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
        }
    }
}

