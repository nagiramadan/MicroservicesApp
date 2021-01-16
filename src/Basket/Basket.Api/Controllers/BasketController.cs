using AutoMapper;
using Basket.Api.Entities;
using Basket.Api.Repositories.Interfaces;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
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
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMQProducer _eventBus;

        public BasketController(IBasketRepository repository, IMapper mapper, EventBusRabbitMQProducer eventBus)
        {
            _repository = repository;
            _mapper = mapper;
            _eventBus = eventBus;
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

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetBasketAsync(basketCheckout.UserName);
            if (basket == null)
                return BadRequest();
            var isRemoved = await _repository.DeleteBasketAsync(basketCheckout.UserName);
            if (!isRemoved)
                return BadRequest();
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;
            _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            return Ok();
        }
    }
}
