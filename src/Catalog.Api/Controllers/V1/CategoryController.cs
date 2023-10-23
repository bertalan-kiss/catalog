using System.Net;
using Catalog.Api.Mappers;
using Catalog.Api.Requests;
using Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Controllers.V1;

[ApiController]
[Route("api/v1/catalog")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    [Route("category")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> ListCategories()
    {
        var result = await categoryService.List();

        return Ok(result);
    }

    [HttpPost]
    [Route("category")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
    {
        await categoryService.Add(CategoryMapper.Map(request));

        return Ok();
    }

    [HttpPut]
    [Route("category")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequest request)
    {
        await categoryService.Update(CategoryMapper.Map(request));

        return Ok();
    }

    [HttpDelete]
    [Route("category")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        await categoryService.Delete(categoryId);

        return Ok();
    }
}

