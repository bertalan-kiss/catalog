namespace Catalog.Domain.Messages
{
    public class ItemUpdatedMessage
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
    }
}
