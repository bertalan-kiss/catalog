using Carting.Infrastructure.DataAccess.Models;

namespace Carting.Infrastructure.DataAccess.Repositories
{
    public interface ICartingRepository
    {
        IList<CartItem> GetCartItems();
        IList<CartItem> GetCartItems(string cartId);
        void AddCartItem(CartItem cartItem);
        bool UpdateCartItem(CartItem cartItem);
        bool RemoveCartItem(string cartId, int cartItemId);
    }
}

