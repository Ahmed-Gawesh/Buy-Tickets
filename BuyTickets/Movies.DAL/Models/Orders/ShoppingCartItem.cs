using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models.Orders
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Movie movie { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
        
    }
}
