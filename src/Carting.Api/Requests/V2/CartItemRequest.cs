using System.ComponentModel.DataAnnotations;

namespace Carting.Api.Requests.V2
{
    public class CartItemRequest
    {
        /// <summary>
        /// Unique identifier of the item in the external Catalog system 
        /// </summary>
        public Guid ExternalId { get; set; }
        /// <summary>
        /// Name of the cart item
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Image data
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// Price of the cart item
        /// </summary>
        [Required]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity of the cart items in the cart
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }

    public class Image
    {
        /// <summary>
        /// Url of the image
        /// </summary>
        [Required]
        public string Url { get; set; }
        /// <summary>
        /// Alt text of the image
        /// </summary>
        [Required]
        public string Alt { get; set; }
    }
}

