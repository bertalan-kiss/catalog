namespace Catalog.Domain.Messages
{
    public class CategoryUpdatedMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
