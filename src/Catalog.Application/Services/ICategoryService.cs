using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface ICategoryService
    {
        Task<int> Add(Category category);
        Task<Category> Get(int id);
        Task<IEnumerable<Category>> List();
        Task Update(Category category);
        Task Delete(int id);
    }
}

