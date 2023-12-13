using Carting.Core.Models;

namespace Carting.Core.Mappers
{
    public static class CartItemMapper
    {
        public static Infrastructure.DataAccess.Models.CartItem Map(CartItem cartItem)
        {
            if (cartItem == null)
                throw new ArgumentNullException(nameof(cartItem));

            return new Infrastructure.DataAccess.Models.CartItem
            {
                CartId = cartItem.CartId,
                _id = cartItem.Id,
                ExternalId = cartItem.ExternalId,
                Image = new Infrastructure.DataAccess.Models.Image
                {
                    Url = cartItem.Image?.Url,
                    Alt = cartItem.Image?.Alt
                },
                Name = cartItem.Name,
                Price = cartItem.Price,
                Quantity = cartItem.Quantity
            };
        }

        public static CartItem Map(Infrastructure.DataAccess.Models.CartItem cartItem)
        {
            if (cartItem == null) 
                throw new ArgumentNullException(nameof(cartItem));

            return new CartItem
            {
                CartId = cartItem.CartId,
                Id = cartItem._id,
                ExternalId = cartItem.ExternalId,
                Image = new Image
                {
                    Url = cartItem.Image.Url,
                    Alt = cartItem.Image.Alt
                },
                Name = cartItem.Name,
                Price = cartItem.Price,
                Quantity = cartItem.Quantity
            };
        }

        public static IEnumerable<CartItem> Map(IList<Infrastructure.DataAccess.Models.CartItem> cartItems)
        {
            if (cartItems == null) 
                throw new ArgumentNullException(nameof(cartItems));

            var result = new List<CartItem>();

            foreach ( var cartItem in cartItems )
            {
                result.Add(Map(cartItem));
            }

            return result;
        }
    }
}

