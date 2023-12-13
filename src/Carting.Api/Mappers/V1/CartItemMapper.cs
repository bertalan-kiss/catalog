using Carting.Api.Requests.V1;
using Carting.Api.Responses.V1;

namespace Carting.Api.Mappers.V1
{
    public static class CartItemMapper
    {
        public static Core.Models.CartItem Map(string cartId, CartItemRequest request)
        {
            var cartItem = new Core.Models.CartItem
            {
                CartId = cartId,
                ExternalId = request.ExternalId,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            };

            if (request.Image != null)
            {
                cartItem.Image = new Core.Models.Image
                {
                    Url = request.Image.Url,
                    Alt = request.Image.Alt
                };
            }

            return cartItem;
        }

        public static CartResponse Map(string cartId, IEnumerable<Core.Models.CartItem> cartItems)
        {
            var items = new List<CartItem>();

            foreach (var cartItem in cartItems)
            {
                items.Add(new CartItem
                {
                    Id = cartItem.Id,
                    ExternalId = cartItem.ExternalId,
                    Name = cartItem.Name,
                    ImageUrl = cartItem.Image?.Url,
                    ImageAlt = cartItem.Image?.Alt,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity
                }); ;
            }

            return new CartResponse
            {
                CartId = cartId,
                CartItems = items
            };
        }
    }
}

