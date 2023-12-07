using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.GraphQl
{
	public class CategoryGraphQlService : ICategoryGraphQlService
	{
        private readonly ICategoryRepository categoryRepository;

        public CategoryGraphQlService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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
    }
}

