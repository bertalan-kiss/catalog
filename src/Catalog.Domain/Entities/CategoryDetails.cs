namespace Catalog.Domain.Entities
{
    public class CategoryDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentId { get; set; }
    }
}

