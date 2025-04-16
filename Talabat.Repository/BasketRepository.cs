using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryContract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer  connection) // ask clr create object from class to implement an InterFace  IConnectionMultiplexer
        {
            _database=connection.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }
        //GET OR Recreate Basket
        public async Task<CustomerBasket?> GetBasketAsunc(string BasketId)
        {
            var Basket= await _database.StringGetAsync(BasketId);
            return  Basket.IsNull? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
        }
        //update and create Basket
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
        {
            var jsonBasket = JsonSerializer.Serialize(Basket);
            var BasketCreateOrUpdated = await _database.StringSetAsync(Basket.Id,jsonBasket,TimeSpan.FromDays(1));
            if(!BasketCreateOrUpdated) return null;
            else 
                return await GetBasketAsunc(Basket.Id);
        }
    }
}
