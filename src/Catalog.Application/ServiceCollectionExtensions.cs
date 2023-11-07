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
            collection.AddScoped<ICategoryService, CategoryService>();
            collection.AddScoped<IItemService, ItemService>();
            return collection;
        }

        public static IServiceCollection AddApplicationValidators(this IServiceCollection collection)
        {
            collection.AddScoped<IValidator<Category>, CategoryValidator>();
            collection.AddScoped<IValidator<Item>, ItemValidator>();
            return collection;
        }
    }
}

