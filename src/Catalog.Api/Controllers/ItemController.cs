using System.Net;
using Catalog.Api.Mappers;
using Catalog.Api.Requests;
using Catalog.Application.Services;
using Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("catalog")]
public class ItemController : ControllerBase
{
    private readonly IItemService itemService;

    public ItemController(IItemService itemService)
    {
        this.itemService = itemService;
    }

    [HttpGet]
    [Route("item/{categoryId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Item>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> ListItems(int categoryId, [BindRequired] int pageSize, [BindRequired] int page)
    {
        var result = await itemService.List(categoryId, pageSize, page);

        return Ok(result);
    }

    [HttpPost]
    [Route("item")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(int))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> AddItem([FromBody] ItemRequest request)
    {
        try
        {
            var result = await itemService.Add(ItemMapper.Map(request));

            return Ok(result);
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("item/{itemId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> UpdateItem(int itemId, [FromBody] ItemRequest request)
    {
        try
        {
            await itemService.Update(ItemMapper.Map(request, itemId));

            return Ok();
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ItemNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    [Route("item/{itemId}")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        try
        {
            await itemService.Delete(itemId);

            return Ok();
        }
        catch (ItemNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}

