using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService service) => _ordersService = service;

        [ActionName(nameof(GetAllAsync))]
        [HttpGet]
        public async Task<List<Order>> GetAllAsync() => await _ordersService.GetAllAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<Order> GetByIdAsync(int id) => await _ordersService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddAsync(int userId)
        {
            Order newOrder = await _ordersService.AddAsync(userId);
            return CreatedAtAction(nameof(GetAllAsync), 
                new { id = newOrder.Id }, newOrder);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Order OrderToRemove = await _ordersService.DeleteAsync(id);
            return CreatedAtAction(nameof(GetAllAsync), 
                new { id = OrderToRemove.Id }, OrderToRemove);
        }

        [Route("api/[controller]/[action]")]
        [HttpPut()]
        public async Task<ActionResult> AddPizzaToOrderAsync(int pizzaId, int orderId)
        {
            Order order = await _ordersService.AddPizzaToOrderAsync(pizzaId, orderId);
            return CreatedAtAction(nameof(GetAllAsync), new { id = order.Id }, order);
        }

        [Route("api/[controller]/[action]")]
        [HttpPut()]
        public async Task<ActionResult> RemovePizzaFromOrderAsync(int pizzaId, int orderId)
        {
            Order order = await _ordersService
                .RemovePizzaFromOrderAsync(pizzaId, orderId);

            return CreatedAtAction(nameof(GetAllAsync), 
                new { id = order.Id }, order);
        }

        [Route("api/[controller]/[action]")]
        [HttpPut()]
        public async Task<ActionResult> AddPromoCodeToOrderAsync(int promoCodeId, int orderId)
        {
            Order order = await _ordersService
                .AddPromoCodeToOrderAsync(promoCodeId, orderId);

            return CreatedAtAction(nameof(GetAllAsync),
                new { id = order.Id }, order);
        }

        [Route("api/[controller]/[action]")]
        [HttpPut()]
        public async Task<ActionResult> RemovePromoCodeFromOrderAsync(int promoCodeId, int orderId)
        {
            Order order = await _ordersService
                .RemovePromoCodeFromOrderAsync(promoCodeId, orderId);

            return CreatedAtAction(nameof(GetAllAsync),
                new { id = order.Id }, order);
        }

        [Route("api/[controller]/[action]")]
        [HttpPut()]
        public async Task<ActionResult> AddAdressToOrderAsync(string adress, int orderId)
        {
            Order order = await _ordersService
                .AddAdressToOrderAsync(adress, orderId);

            return CreatedAtAction(nameof(GetAllAsync),
                new { id = order.Id }, order);
        }

        [Route("api/[controller]/[action]")]
        [HttpPut()]
        public async Task<ActionResult> RemoveAdressFromOrderAsync(string adress, int orderId)
        {
            Order order = await _ordersService
                .RemoveAdressFromOrderAsync(adress, orderId);

            return CreatedAtAction(nameof(GetAllAsync),
                new { id = order.Id }, order);
        }
    }
}
