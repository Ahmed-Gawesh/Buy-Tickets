using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Models.Orders
{
	public class BasketItem
	{
		public int Id { get; set; }
		public Movie Movie { get; set; }
		public int Quantity { get; set; }

		public decimal Total { get; set; }

        public BasketItem()
        {
			Total = Quantity + Movie.Price;
        }
    }
}
