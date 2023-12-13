namespace Carting.Core.Models
{
    public class CartItem
    {
        public string CartId { get; set; }
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

