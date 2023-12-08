using GraphQL.AspNet.Attributes;
using GraphQL.AspNet.Controllers;
using Catalog.Domain.Entities;
using Catalog.Application.Services;
using Catalog.Api.Requests;
using Catalog.Api.Mappers;

namespace Catalog.Api.Controllers.GraphQL
{
    public class CatalogController : GraphController
    {
        private readonly ICategoryService categoryService;
        private readonly IItemService itemService;

        public CatalogController(ICategoryService categoryService, IItemService itemService)
        {
            this.categoryService = categoryService;
            this.itemService = itemService;
        }

        [QueryRoot("categories")]
        public async Task<IEnumerable<Category>> ListCategories()
        {
            return await categoryService.List();
        }

        [QueryRoot("items")]
        public async Task<IEnumerable<Item>> ListItems(int categoryId, int pageSize, int page)
        {
            return await itemService.List(categoryId, pageSize, page);
        }

        [MutationRoot("addCategory")]
        public async Task<int?> AddCategory(CategoryRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return null;

            var category = CategoryMapper.Map(request);
            return await categoryService.Add(category);
        }

        [MutationRoot("addItem")]
        public async Task<int?> AddItem(ItemRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return null;

            var item = ItemMapper.Map(request);
            return await itemService.Add(item);
        }

        [MutationRoot("updateCategory")]
        public async Task<int?> UpdateCategory(int categoryId, CategoryRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return null;

            var category = CategoryMapper.Map(request, categoryId);
            await categoryService.Update(category);

            return categoryId;
        }

        [MutationRoot("updateItem")]
        public async Task<int?> UpdateItem(int itemId, ItemRequest request)
        {
            if (!ModelState.IsValid || request == null)
                return null;

            var item = ItemMapper.Map(request, itemId);
            await itemService.Update(item);

            return itemId;
        }

        [MutationRoot("deleteItem")]
        public async Task<int?> DeleteItem(int itemId)
        {
            await itemService.Delete(itemId);

            return itemId;
        }

        [MutationRoot("deleteCategory")]
        public async Task<int?> DeleteCategory(int categoryId)
        {
            await categoryService.Delete(categoryId);

            return categoryId;
        }
    }
}

