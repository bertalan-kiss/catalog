using System.Net;
using Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/v1/catalog")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly ICategoryService categoryService;

    public CatalogController(ILogger<CatalogController> logger, ICategoryService categoryService)
    {
        _logger = logger;
        this.categoryService = categoryService;
    }

    [HttpGet]
    [Route("category")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> Get()
    {
        var result = await categoryService.List();

        return Ok(result);
    }
}

