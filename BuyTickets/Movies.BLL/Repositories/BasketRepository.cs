using Movies.BLL.Interfaces;
using Movies.DAL.Models.Orders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.BLL.Repositories
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database= redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string id)
		{
			return await _database.KeyDeleteAsync(id);
		}

		public async Task<CustomerBasket> GetBasketAsync(string id)
		{
			var basket=await _database.StringGetAsync(id);
			return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket> UpdateBasketAsyc(CustomerBasket basket)
		{
			var CreatedOrUpdated = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(1));
			if (!CreatedOrUpdated) return null;
			return await GetBasketAsync(basket.Id);

		}
	}
}
