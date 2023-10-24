using Catalog.Api.Requests;

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
    }
}

