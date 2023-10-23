using Catalog.Api.Requests;

namespace Catalog.Api.Mappers
{
	public static class ItemMapper
	{
		public static Domain.Entities.Item Map(AddItemRequest request)
		{
            return new Domain.Entities.Item
            {
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

        public static Domain.Entities.Item Map(UpdateItemRequest request)
        {
            return new Domain.Entities.Item
            {
                Id = request.Id,
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

