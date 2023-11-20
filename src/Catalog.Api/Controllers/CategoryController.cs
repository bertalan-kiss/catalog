using System.Data;
using System.Net;
using Catalog.Api.Mappers;
using Catalog.Api.Requests;
using Catalog.Api.Responses;
using Catalog.Application.Services;
using Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("catalog/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [Authorize(Roles = "manager,buyer")]
    [HttpGet]
    [Route("{categoryId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(CategoryResponse))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> GetCategory(int categoryId)
    {
        try
        {
            var result = await categoryService.Get(categoryId);
            return Ok(CategoryMapper.Map(result));
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = "manager,buyer")]
    [HttpGet]
    [Route("")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<CategoryResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> ListCategories()
    {
        try
        {
            var result = await categoryService.List();
            return Ok(CategoryMapper.Map(result));
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [Authorize(Roles = "manager")]
    [HttpPost]
    [Route("")]
    [SwaggerResponse((int)HttpStatusCode.Created, nameof(HttpStatusCode.Created), typeof(CategoryResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, nameof(HttpStatusCode.Conflict), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> AddCategory([FromBody] CategoryRequest request)
    {
        try
        {
            var category = CategoryMapper.Map(request);
            category.Id = await categoryService.Add(category);

            return Created($"/catalog/category/{category.Id}", CategoryMapper.Map(category));
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (CategoryConflictException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [Authorize(Roles = "manager")]
    [HttpPut]
    [Route("{categoryId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(CategoryResponse))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, nameof(HttpStatusCode.Conflict), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryRequest request)
    {
        try
        {
            var category = CategoryMapper.Map(request, categoryId);
            await categoryService.Update(category);

            return Ok(CategoryMapper.Map(category));
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (CategoryConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = "manager")]
    [HttpDelete]
    [Route("{categoryId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, nameof(HttpStatusCode.Conflict), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        try
        {
            await categoryService.Delete(categoryId);
            return Ok();
        }
        catch (CategoryConflictException ex)
        {
            return Conflict(ex.Message);
        }
        catch (CategoryNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

