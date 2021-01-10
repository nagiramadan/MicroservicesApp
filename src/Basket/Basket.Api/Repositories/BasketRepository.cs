using Basket.Api.Data.Interfaces;
using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;

        public BasketRepository(IBasketContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBasketAsync(string userName)
        {
            var deleted = await _context.Redis.KeyDeleteAsync(userName);
            return deleted;
        }

        public async Task<BasketCart> GetBasketAsync(string userName)
        {
            var basketString = await _context.Redis.StringGetAsync(userName);
            if (basketString.IsNullOrEmpty)
                return null;
            return JsonConvert.DeserializeObject<BasketCart>(basketString);
        }

        public async Task<BasketCart> UpdateBasketAsync(BasketCart basket)
        {
            var updated = await _context.Redis.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            if (!updated)
                return null;
            return await GetBasketAsync(basket.UserName);
        }
    }
}
