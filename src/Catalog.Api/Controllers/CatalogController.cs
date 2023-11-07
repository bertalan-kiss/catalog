using Catalog.Api.Responses;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Link>))]
        public IActionResult Root() => Ok(new List<Link>
        {
            new Link
            {
                Href = $"/catalog",
                Rel = "self",
                Method = "GET"
            },
            new Link
            {
                Href = $"/catalog/category",
                Rel = "list_category",
                Method = "GET"
            },
            new Link
            {
                Href = $"/catalog/category",
                Rel = "create_category",
                Method = "POST"
            },
            new Link
            {
                Href = $"/catalog/item",
                Rel = "create_item",
                Method = "POST"
            }
        });
    }
}

