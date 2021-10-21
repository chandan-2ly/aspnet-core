using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentException(nameof(redisCache));
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            ShoppingCart itemsToAdd = basket;
            var cacheItems = await _redisCache.GetStringAsync(basket.UserName);
            
            if (cacheItems != null)
            {
                var existingItems = JsonConvert.DeserializeObject<ShoppingCart>(cacheItems);
                if (existingItems != null)
                {
                    itemsToAdd.Items.AddRange(existingItems.Items);
                }
            }
            

            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(itemsToAdd));
            return await GetBasket(basket.UserName);
        }
    }
}
