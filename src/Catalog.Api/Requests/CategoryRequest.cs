namespace Catalog.Api.Requests
{
    public class CategoryRequest
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

