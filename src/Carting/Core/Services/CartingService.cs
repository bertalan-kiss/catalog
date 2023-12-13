using Carting.Core.Mappers;
using Carting.Core.Models;
using Carting.Infrastructure.DataAccess.Repositories;

namespace Carting.Core.Services
{
    public class CartingService : ICartingService
	{
        private readonly ICartingRepository _cartingRepository;

        public CartingService(ICartingRepository cartingRepository)
        {
            _cartingRepository = cartingRepository;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            return CartItemMapper.Map(_cartingRepository.GetCartItems());
        }

        public IEnumerable<CartItem> GetCartItems(string cartId)
        {
            return CartItemMapper.Map(_cartingRepository.GetCartItems(cartId));
        }

        public void AddCartItem(CartItem cartItem)
        {
            _cartingRepository.AddCartItem(CartItemMapper.Map(cartItem));
        }

        public bool UpdateCartItem(CartItem cartItem)
        {
            return _cartingRepository.UpdateCartItem(CartItemMapper.Map(cartItem));
        }

        public bool RemoveCartItem(string cartId, int cartItemId)
        {
            return _cartingRepository.RemoveCartItem(cartId, cartItemId);
        }
    }
}

