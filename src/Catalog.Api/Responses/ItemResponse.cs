namespace Catalog.Api.Responses
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public IList<Link> Links { get; set; }
    }
}

