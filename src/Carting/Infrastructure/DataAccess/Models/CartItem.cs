namespace Carting.Infrastructure.DataAccess.Models
{
    public class CartItem
    {
        public string CartId { get; set; }
        public int _id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

