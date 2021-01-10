using Basket.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasketAsync(string userName);
        Task<BasketCart> UpdateBasketAsync(BasketCart basket);
        Task<bool> DeleteBasketAsync(string userName);
    }
}
