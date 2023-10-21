using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ILogger<CatalogController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetCatalog")]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}

