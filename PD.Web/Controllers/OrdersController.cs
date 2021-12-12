using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Models;
using PD.Domain.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService service, IMapper mapper)
        {
            _ordersService = service;
        }

        [Authorize(Roles = "Administrator")]
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        { 
            return Ok(await _ordersService.GetAllAsync());
        }

        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _ordersService.GetByIdAsync(id));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddOrderViewModel model)
        {
            return Ok(await _ordersService.AddAsync(model));
        }

        [Authorize(Roles = "Administrator")]
        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _ordersService.DeleteAsync(id));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _ordersService.DeleteAsync(long.Parse(userId)));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddPizzaAsync(long pizzaId, long orderId)
        {
            return Ok(await _ordersService.AddPizzaAsync(pizzaId, orderId));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemovePizzaAsync(long pizzaId, long orderId)
        {
            return Ok(await _ordersService.RemovePizzaAsync(pizzaId, orderId));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddPromoCodeAsync(long promoCodeId, long orderId)
        {
            return Ok(await _ordersService.AddPromoCodeAsync(promoCodeId, orderId));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemovePromoCodeAsync(long orderId)
        {
            return Ok(await _ordersService.RemovePromoCodeAsync(orderId));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddAdressAsync(string adress, long orderId)
        {
            return Ok(await _ordersService.AddAdressAsync(adress, orderId));
        }

        [Authorize(Roles = "User")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemoveAdressAsync(long orderId)
        {
            return Ok(await _ordersService.RemoveAdressAsync(orderId));
        }
    }
}
