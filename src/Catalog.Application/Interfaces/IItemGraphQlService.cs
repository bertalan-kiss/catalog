using Catalog.Domain.Entities;

namespace Catalog.Application.Interfaces
{
	public interface IItemGraphQlService
    {
        Task<IEnumerable<ItemDetails>> GetItemList(int categoryId, int pageSize, int page);
    }
}

