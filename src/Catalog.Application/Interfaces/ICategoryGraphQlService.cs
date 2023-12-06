using Catalog.Domain.Entities;

namespace Catalog.Application.Interfaces
{
	public interface ICategoryGraphQlService
	{
		Task<IList<CategoryDetails>> GetCategoryList();
        Task<IEnumerable<ItemDetails>> GetItemList(int categoryId, int pageSize, int page);
    }
}

