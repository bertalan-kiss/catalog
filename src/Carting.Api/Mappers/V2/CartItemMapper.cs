using Carting.Api.Requests.V2;
using Carting.Api.Responses.V2;

namespace Carting.Api.Mappers.V2
{
    public static class CartItemMapper
    {
        public static Core.Models.CartItem Map(string cartId, CartItemRequest request)
        {
            var cartItem = new Core.Models.CartItem
            {
                CartId = cartId,
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

        public static IEnumerable<CartItem> Map(IEnumerable<Core.Models.CartItem> cartItems)
        {
            var items = new List<CartItem>();

            foreach (var cartItem in cartItems)
            {
                items.Add(new CartItem
                {
                    CartId = cartItem.CartId,
                    Id = cartItem.Id,
                    Name = cartItem.Name,
                    ImageUrl = cartItem.Image?.Url,
                    ImageAlt = cartItem.Image?.Alt,
                    Price = cartItem.Price,
                    Quantity = cartItem.Quantity
                }); ;
            }

            return items;
        }
    }
}

