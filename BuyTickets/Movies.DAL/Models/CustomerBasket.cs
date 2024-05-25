using Movies.DAL.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<OrderItems> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecrete { get; set; }

        public CustomerBasket(string id)
        {
            Id=id;
        }
    }
}
