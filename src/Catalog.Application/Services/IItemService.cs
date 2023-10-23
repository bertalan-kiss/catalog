using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface IItemService
    {
        Task<int> Add(Item item);
        Task<Item> Get(int id);
        Task<IEnumerable<Item>> List();
        Task Update(Item item);
        Task Delete(int id);
    }
}

