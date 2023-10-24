namespace Catalog.Api.Responses
{
	public class CategoryResponse
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }
        public IList<Link> Links { get; set; }
    }
}