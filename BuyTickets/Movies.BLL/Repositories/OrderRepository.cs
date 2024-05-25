using Microsoft.EntityFrameworkCore;
using Movies.BLL.Interfaces;
using Movies.DAL;
using Movies.DAL.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MovieContext dbContext;

        public OrderRepository(MovieContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task SaveOrderAsync(List<ShoppingCartItem> items, string userId, string buyerEmail)
        {
           var order = new Order()
           {
               UserId = userId, 
               BuyerEmail = buyerEmail,
           }; 
            await dbContext.AddAsync(order);
            await dbContext.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItems()
                {
                    Amount = item.Amount,
                    MovieId = item.movie.Id,
                    OrderId = order.Id,
                    Price = item.movie.Price,
                };
                await dbContext.AddAsync(orderItem);
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrderForUserAsync(string buyerEmail)
        {
           var orders=await dbContext.Orders.Include(n=>n.OrderItems)
                .ThenInclude(n=>n.Movie).Include(n=>n.User).ToListAsync();

            return orders;
            
        }


    }
}
