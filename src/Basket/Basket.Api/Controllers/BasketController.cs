using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetBasket(string userName)
        {
            var basket = await _repository.GetBasketAsync(userName);
            return Ok(basket);
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateBasket(BasketCart basket)
        {
            basket = await _repository.UpdateBasketAsync(basket);
            return Ok(basket);
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBakset(string userName)
        {
            var deleted = await _repository.DeleteBasketAsync(userName);
            return Ok(deleted);
        }
    }
}
