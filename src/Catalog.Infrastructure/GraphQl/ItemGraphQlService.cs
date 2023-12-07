using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.GraphQl
{
	public class ItemGraphQlService : IItemGraphQlService
    {
        private readonly IItemRepository itemRepository;

        public ItemGraphQlService(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public async Task<IEnumerable<ItemDetails>> GetItemList(int categoryId, int pageSize, int page)
        {
            var items = await itemRepository.List(categoryId, pageSize, page);

            return items.Select(i => new ItemDetails
            {
                Id = i.Id,
                Identifier = i.Identifier,
                Name = i.Name,
                Description = i.Description,
                ImageUrl = i.ImageUrl,
                CategoryId = i.Category?.Id,
                Amount = i.Amount,
                Price = i.Price
            });
        }
    }
}

