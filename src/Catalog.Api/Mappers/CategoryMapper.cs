using Catalog.Api.Requests;

namespace Catalog.Api.Mappers
{
    public static class CategoryMapper
    {
        public static Domain.Entities.Category Map(AddCategoryRequest request)
        {
            return new Domain.Entities.Category
            {
                Name = request.Name,
                ImageUrl = request.ImageUrl,
                Parent = request.ParentCategoryId != null ? new Domain.Entities.Category
                {
                    Id = (int)request.ParentCategoryId
                } : null
            };
        }

        public static Domain.Entities.Category Map(UpdateCategoryRequest request)
        {
            return new Domain.Entities.Category
            {
                Id = request.Id,
                Name = request.Name,
                ImageUrl = request.ImageUrl,
                Parent = request.ParentCategoryId != null ? new Domain.Entities.Category
                {
                    Id = (int)request.ParentCategoryId
                } : null
            };
        }
    }
}

