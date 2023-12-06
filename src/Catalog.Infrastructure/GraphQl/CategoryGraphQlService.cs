using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.GraphQl
{
	public class CategoryGraphQlService : ICategoryGraphQlService
	{
        private readonly ICategoryRepository categoryRepository;
        private readonly IItemRepository itemRepository;

        public CategoryGraphQlService(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            this.categoryRepository = categoryRepository;
            this.itemRepository = itemRepository;
        }

        public async Task<IList<CategoryDetails>> GetCategoryList()
        {
            var categories = await categoryRepository.List();

            return categories.Select(c => new CategoryDetails
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                ParentId = c.Parent?.Id
            }).ToList();
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

