using Carting.Api.Mappers.V2;
using Carting.Api.Requests.V2;
using Carting.Core.Services;
using Carting.Infrastructure.DataAccess.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Carting.Api.Controllers.V2;

[ApiController]
[ApiVersion("2.0")]
[ApiExplorerSettings(GroupName = "v2")]
[Route("api/v{version:apiVersion}")]
public class CartingController : ControllerBase
{
    private readonly ICartingService cartingService;

    public CartingController(ICartingService cartingService)
    {
        this.cartingService = cartingService;
    }

    /// <summary>
    /// Returns all cart items.
    /// </summary>
    /// <returns>Cart items.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /cart
    ///
    /// </remarks>
    /// <response code="200">Cart items returned successfully</response>
    /// <response code="500">Internal server error occured</response>
    [HttpGet]
    [Route("cart")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Get()
    {
        try
        {
            var result = cartingService.GetCartItems();

            return Ok(CartItemMapper.Map(result));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Creates a cart item in the cart for the provided cartId.
    /// </summary>
    /// <param name="cartId">Identifier of the cart.</param>
    /// <param name="request">Cart item request.</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Cart/1
    ///     {
    ///        "name": "string",
    ///        "image": {
    ///             "url": "string",
    ///             "alt": "string",
    ///        },
    ///        "price": 0,
    ///        "quantity": 0
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Cart items returned successfully</response>
    /// <response code="400">Bad request</response>
    /// <response code="500">Internal server error occured</response>
    [HttpPost]
    [Route("cart/{cartId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post(string cartId, CartItemRequest request)
    {
        try
        {
            cartingService.AddCartItem(CartItemMapper.Map(cartId, request));

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    /// <summary>
    /// Deletes a cart item from the cart based on the provided cartId.
    /// </summary>
    /// <param name="cartId">Identifier of the cart.</param>
    /// <param name="itemId">Identifier of the cart item.</param>
    /// <returns></returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     DELETE /cart/1?itemId=1
    ///
    /// </remarks>
    /// <response code="200">Cart item deleted successfully</response>
    /// <response code="404">Cart or cart item not found</response>
    /// <response code="500">Internal server error occured</response>
    [HttpDelete]
    [Route("cart/{cartId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(string cartId, [BindRequired] int itemId)
    {
        try
        {
            var result = cartingService.RemoveCartItem(cartId, itemId);

            if (result)
                return Ok();
            else
                return StatusCode(500, $"Failed to delete cart item with id: {itemId}, cart id: {cartId}");
        }
        catch (CartItemNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}

