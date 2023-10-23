using System.Net;
using Catalog.Api.Mappers;
using Catalog.Api.Requests;
using Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Catalog.Api.Controllers.V1;

[ApiController]
[Route("api/v1/catalog")]
public class ItemController : ControllerBase
{
    private readonly IItemService itemService;

    public ItemController(IItemService itemService)
    {
        this.itemService = itemService;
    }

    [HttpGet]
    [Route("item")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> ListItems(int? categoryId, int? pageSize, int? page)
    {
        var result = await itemService.List(categoryId, pageSize, page);

        return Ok(result);
    }

    [HttpPost]
    [Route("item")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> AddItem([FromBody] AddItemRequest request)
    {
        await itemService.Add(ItemMapper.Map(request));

        return Ok();
    }

    [HttpPut]
    [Route("item")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> UpdateItem([FromBody] UpdateItemRequest request)
    {
        await itemService.Update(ItemMapper.Map(request));

        return Ok();
    }

    [HttpDelete]
    [Route("item")]
    [SwaggerResponse((int)HttpStatusCode.OK, nameof(HttpStatusCode.OK), typeof(IEnumerable<Domain.Entities.Category>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, nameof(HttpStatusCode.InternalServerError))]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        await itemService.Delete(itemId);

        return Ok();
    }
}

