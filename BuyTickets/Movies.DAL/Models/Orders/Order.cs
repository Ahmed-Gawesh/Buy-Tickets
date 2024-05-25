using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models.Orders
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public List<OrderItems> OrderItems { get; set; }

    }
}
