namespace Catalog.Api.Requests
{
	public class UpdateCategoryRequest
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

