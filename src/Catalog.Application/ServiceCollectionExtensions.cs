using Catalog.Application.Services;
using Catalog.Application.Validators;
using Catalog.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection collection)
        {
            collection.AddSingleton<ICategoryService, CategoryService>();
            collection.AddSingleton<IItemService, ItemService>();
            return collection;
        }

        public static IServiceCollection AddApplicationValidators(this IServiceCollection collection)
        {
            collection.AddSingleton<IValidator<Category>, CategoryValidator>();
            collection.AddSingleton<IValidator<Item>, ItemValidator>();
            return collection;
        }
    }
}

