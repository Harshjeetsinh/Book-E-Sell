using BookStoreM.models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class CartRepository : BaseRepository
    {
        public List<Cart> GetCartItems(string keyword)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = context.Carts.Include(c => c.Book).Where(c => keyword == null || c.Book.Name.ToLower().Contains(keyword)).AsQueryable();
            return query.ToList();
        }

        public Cart GetCarts(int id)
        {
            return context.Carts.FirstOrDefault(c => c.Id == id);
        }

        public Cart AddCart(Cart category)
        {
            var entry = context.Carts.Add(category);
            context.SaveChanges();
            return entry.Entity;
        }

        public Cart UpdateCart(Cart category)
        {
            var entry = context.Carts.Update(category);
            context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteCart(int id)
        {
            var cart = context.Carts.FirstOrDefault(c => c.Id == id);
            if (cart == null)
                return false;

            context.Carts.Remove(cart);
            context.SaveChanges();
            return true;
        }
    }
}
