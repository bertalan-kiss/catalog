using Carting.Infrastructure.DataAccess.Exceptions;
using Carting.Infrastructure.DataAccess.Models;
using LiteDB;

namespace Carting.Infrastructure.DataAccess.Repositories
{
    public class CartingRepository : ICartingRepository
    {
        private const string DatabasePath = "CartingDatabase.db";
        private const string CartItemsTableName = "cart_items";

        public IList<CartItem> GetCartItems()
        {
            using var db = new LiteDatabase(DatabasePath);

            var collection = db.GetCollection<CartItem>(CartItemsTableName);

            return collection.FindAll().ToList();
        }

        public IList<CartItem> GetCartItems(string cartId)
        {
            using var db = new LiteDatabase(DatabasePath);

            var collection = db.GetCollection<CartItem>(CartItemsTableName);

            return collection.Find(x => x.CartId == cartId).ToList();
        }

        public void AddCartItem(CartItem cartItem)
        {
            using var db = new LiteDatabase(DatabasePath);

            var collection = db.GetCollection<CartItem>(CartItemsTableName);
            collection.Insert(cartItem);
        }

        public bool UpdateCartItem(CartItem cartItem)
        {
            using var db = new LiteDatabase(DatabasePath);

            var collection = db.GetCollection<CartItem>(CartItemsTableName);

            var item = collection.FindOne(x => x.ExternalId == cartItem.ExternalId);

            if (item == null)
            {
                return false;
            }

            item.Name = cartItem.Name;
            item.Image = cartItem.Image;
            item.Price = cartItem.Price;
            item.Quantity = cartItem.Quantity;

            return collection.Update(item);
        }

        public bool RemoveCartItem(string cartId, int cartItemId)
        {
            using var db = new LiteDatabase(DatabasePath);

            var collection = db.GetCollection<CartItem>(CartItemsTableName);

            var item = collection.FindOne(x => x.CartId == cartId && x._id == cartItemId);

            if (item == null)
            {
                throw new CartItemNotFoundException($"Cart item not found with id: {cartItemId}, cart id: {cartId}");
            }

            return collection.Delete(cartItemId);
        }
    }
}

