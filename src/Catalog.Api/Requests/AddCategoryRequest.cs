namespace Catalog.Api.Requests
{
    public class AddCategoryRequest
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

