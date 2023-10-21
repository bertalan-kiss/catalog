using Catalog.Application.Interfaces;
using Catalog.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection collection)
        {
            collection.AddScoped<ICategoryRepository, CategoryRepository>();
            collection.AddScoped<IItemRepository, ItemRepository>();
            return collection;
        }
    }
}

