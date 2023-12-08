using System;
using GraphQL.AspNet.Attributes;
using System.Drawing;
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
    }
}

