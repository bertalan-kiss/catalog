using Carting.Core.Models;

namespace Carting.Core.Services
{
    public interface ICartingService
    {
        IEnumerable<CartItem> GetCartItems();
        IEnumerable<CartItem> GetCartItems(string cartId);
        void AddCartItem(CartItem cartItem);
        bool UpdateCartItem(CartItem cartItem);
        bool RemoveCartItem(string cartId, int cartItemId);
    }
}

