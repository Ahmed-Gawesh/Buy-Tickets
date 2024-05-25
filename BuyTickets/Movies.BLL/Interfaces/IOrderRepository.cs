using Movies.DAL.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Interfaces
{
    public interface IOrderRepository
    {
        Task SaveOrderAsync(List<ShoppingCartItem> items, string userId, string buyerEmail);

         Task<List<Order>> GetOrderForUserAsync(string buyerEmail);

        

    }
}
