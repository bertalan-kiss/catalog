using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Requests
{
    public class ItemRequest
    {
        public Guid? Identifier { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}

