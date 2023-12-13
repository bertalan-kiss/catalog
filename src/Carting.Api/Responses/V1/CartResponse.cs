namespace Carting.Api.Responses.V1
{
    public class CartResponse
    {
        /// <summary>
        /// Identifier of the cart
        /// </summary>
		public string CartId { get; set; }
        /// <summary>
        /// List of cart items
        /// </summary>
		public IEnumerable<CartItem> CartItems { get; set; }
    }

    public class CartItem
    {
        /// <summary>
        /// Id of the cart item
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Unique identifier of the item in the external Catalog system 
        /// </summary>
        public Guid ExternalId { get; set; }
        /// <summary>
        /// Name of the cart item
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Url of the image
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// Alt text of the image
        /// </summary>
        public string ImageAlt { get; set; }
        /// <summary>
        /// Price of the cart item
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the cart items in the cart
        /// </summary>
        public int Quantity { get; set; }
    }
}

