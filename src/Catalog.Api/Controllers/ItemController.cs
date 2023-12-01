using System.Data;
using System.Net;
using Catalog.Api.Mappers;
using Catalog.Api.Requests;
using Catalog.Api.Responses;
using Catalog.Application.Services;
using Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("catalog/item")]
public class ItemController : ControllerBase
{
    private readonly IItemService itemService;

    public ItemController(IItemService itemService)
    {
        this.itemService = itemService;
    }

    [HttpGet]
    [Route("{categoryId}")]
    [Authorize(Roles = "CatalogApi.Read.All")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<ItemResponse>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> ListItems(int categoryId, [BindRequired] int pageSize, [BindRequired] int page)
    {
        var result = await itemService.List(categoryId, pageSize, page);

        return Ok(ItemMapper.Map(result));
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "CatalogApi.Write.All")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(ItemResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> AddItem([FromBody] ItemRequest request)
    {
        try
        {
            var item = ItemMapper.Map(request);
            item.Id = await itemService.Add(item);

            return Ok(ItemMapper.Map(item));
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Route("{itemId}")]
    [Authorize(Roles = "CatalogApi.Write.All")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(ItemResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, nameof(HttpStatusCode.BadRequest))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, nameof(HttpStatusCode.NotFound), typeof(string))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> UpdateItem(int itemId, [FromBody] ItemRequest request)
    {
        try
        {
            var item = ItemMapper.Map(request, itemId);
            await itemService.Update(item);

            return Ok(ItemMapper.Map(item));
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
    [Route("{itemId}")]
    [Authorize(Roles = "CatalogApi.Write.All")]
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

