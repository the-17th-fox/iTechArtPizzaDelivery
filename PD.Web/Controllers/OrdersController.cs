using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Domain.Services.Orders;
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
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService service) => _ordersService = service;

        [ActionName(nameof(GetAllOrdersAsync))]
        [HttpGet]
        public async Task<List<Order>> GetAllOrdersAsync() => await _ordersService.GetAllAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<Order> GetOrderAsync(int id) => await _ordersService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddOrderAsync(int userId, int pizzaId, string adress, int? promoCodeId = null)
        {
            Order newOrder = await _ordersService.AddAsync(userId, pizzaId, adress, promoCodeId);

            return CreatedAtAction(nameof(GetAllOrdersAsync), new { id = newOrder.OrderID }, newOrder);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteOrderAsync(int id)
        {
            Order OrderToRemove = await _ordersService.DeleteAsync(id);

            return CreatedAtAction(nameof(GetAllOrdersAsync), new { id = OrderToRemove.OrderID }, OrderToRemove);
        }
    }
}
