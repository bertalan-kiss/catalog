using Catalog.Domain.Entities;

namespace Catalog.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<int> Add(Item item);
        Task<Item> Get(int id);
        Task<IEnumerable<Item>> List();
        Task Update(Item item);
        Task Delete(int id);
    }
}

