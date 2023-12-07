using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.Requests
{
    public class CategoryRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}

