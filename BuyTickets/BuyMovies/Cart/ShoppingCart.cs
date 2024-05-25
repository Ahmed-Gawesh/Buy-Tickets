using Movies.DAL.Models.Orders;
using Movies.DAL.Models;
using System;
using Movies.DAL;
using Microsoft.EntityFrameworkCore;

namespace BuyMovies.Cart
{
    public class ShoppingCart
    {
        private readonly MovieContext dbContext;


        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(MovieContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<MovieContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = dbContext.shoppingCartItems.FirstOrDefault(n => n.movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    movie = movie,
                    Amount = 1
                };

                dbContext.shoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            dbContext.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = dbContext.shoppingCartItems.FirstOrDefault(n => n.movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    dbContext.shoppingCartItems.Remove(shoppingCartItem);
                }
            }
            dbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = dbContext.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.movie).ToList());
        }

        public decimal GetShoppingCartTotal() => dbContext.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.movie.Price * n.Amount).Sum();

        public async Task ClearShoppingCartAsync()
        {
            var items = await dbContext.shoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            dbContext.shoppingCartItems.RemoveRange(items);
            await dbContext.SaveChangesAsync();
        }
    }
}
   
