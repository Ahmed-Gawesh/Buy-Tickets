using Movies.DAL.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Interfaces
{
	public interface IBasketRepository
	{
		public Task<bool> DeleteBasketAsync(string id);
		public Task<CustomerBasket> GetBasketAsync(string id);
		public Task<CustomerBasket> UpdateBasketAsyc(CustomerBasket basket);
	}
}
