using System;
using Catalog.Domain.Entities;

namespace Catalog.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<int> Add(Category category);
        Task<Category> Get(int id);
        Task<IEnumerable<Category>> List();
        Task Update(Category category);
        Task Delete(int id);
    }
}

