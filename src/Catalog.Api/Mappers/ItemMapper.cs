using Catalog.Api.Requests;
using Catalog.Api.Responses;

namespace Catalog.Api.Mappers
{
    public static class ItemMapper
    {
        public static Domain.Entities.Item Map(ItemRequest request, int? itemId = null)
        {
            return new Domain.Entities.Item
            {
                Id = itemId ?? 0,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Category = new Domain.Entities.Category
                {
                    Id = request.CategoryId
                },
                Price = request.Price,
                Amount = request.Amount
            };
        }

        public static ItemResponse Map(Domain.Entities.Item item)
        {
            return new ItemResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
                CategoryId = item.Category.Id,
                Price = item.Price,
                Amount = item.Amount,
                Links = new List<Link>
                {
                    new Link
                    {
                        Href = $"/catalog/item/{item.Id}",
                        Rel = "update_item",
                        Method = "PUT"
                    },
                    new Link
                    {
                        Href = $"/catalog/item/{item.Id}",
                        Rel = "delete_item",
                        Method = "DELETE"
                    },
                    new Link
                    {
                        Href = $"/catalog/item/{item.Category.Id}{{?pageSize,page}}",
                        Rel = "list_item",
                        Method = "GET"
                    }
                }
            };
        }

        public static IEnumerable<ItemResponse> Map(IEnumerable<Domain.Entities.Item> item)
        {
            return item.Select(Map).ToList();
        }
    }
}

