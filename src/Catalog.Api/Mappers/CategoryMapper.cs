using Catalog.Api.Requests;
using Catalog.Api.Responses;

namespace Catalog.Api.Mappers
{
    public static class CategoryMapper
    {
        public static Domain.Entities.Category Map(CategoryRequest request, int? categoryId = null)
        {
            return new Domain.Entities.Category
            {
                Id = categoryId ?? 0,
                Name = request.Name,
                ImageUrl = request.ImageUrl,
                Parent = request.ParentCategoryId != null ? new Domain.Entities.Category
                {
                    Id = (int)request.ParentCategoryId
                } : null
            };
        }

        public static CategoryResponse Map(Domain.Entities.Category category)
        {
            var response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                ImageUrl = category.ImageUrl,
                ParentCategoryId = category.Parent?.Id,
                Links = new List<Link>
                {
                    new Link
                    {
                        Href = $"/catalog/category/{category.Id}",
                        Rel = "self",
                        Method = "GET"
                    },
                    new Link
                    {
                        Href = $"/catalog/category/{category.Id}",
                        Rel = "update_category",
                        Method = "PUT"
                    },
                    new Link
                    {
                        Href = $"/catalog/category/{category.Id}",
                        Rel = "delete_category",
                        Method = "DELETE"
                    },
                    new Link
                    {
                        Href = $"/catalog/item/{category.Id}{{?pageSize,page}}",
                        Rel = "list_item",
                        Method = "GET"
                    }
                }
            };

            if (category.Parent != null)
            {
                response.Links.Add(new Link
                {
                    Href = $"/catalog/category/{category.Parent.Id}",
                    Rel = "parent_category",
                    Method = "GET"
                });
            }

            return response;
        }

        public static IEnumerable<CategoryResponse> Map(IEnumerable<Domain.Entities.Category> category)
        {
            return category.Select(Map).ToList();
        }
    }
}

