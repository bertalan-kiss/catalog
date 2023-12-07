using Catalog.Domain.Entities;

namespace Catalog.Application.Interfaces
{
	public interface ICategoryGraphQlService
	{
		Task<IList<CategoryDetails>> GetCategoryList();
    }
}

