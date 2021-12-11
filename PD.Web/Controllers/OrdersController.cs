using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Models;
using PD.Domain.Services;
using System;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Authorize(Policy = "DefaultRights")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService service, IMapper mapper)
        {
            _ordersService = service;
            _mapper = mapper;
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

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddOrderViewModel model)
        {
            return Ok(await _ordersService.AddAsync(model));
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _ordersService.DeleteAsync(id));
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            throw new NotImplementedException();
            //This methods stands for order deletion by user
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddPizzaAsync(long pizzaId, long orderId)
        {
            return Ok(await _ordersService.AddPizzaAsync(pizzaId, orderId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemovePizzaAsync(long pizzaId, long orderId)
        {
            return Ok(await _ordersService.RemovePizzaAsync(pizzaId, orderId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddPromoCodeAsync(long promoCodeId, long orderId)
        {
            return Ok(await _ordersService.AddPromoCodeAsync(promoCodeId, orderId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemovePromoCodeAsync(long orderId)
        {
            return Ok(await _ordersService.RemovePromoCodeAsync(orderId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddAdressAsync(string adress, long orderId)
        {
            return Ok(await _ordersService.AddAdressAsync(adress, orderId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemoveAdressAsync(long orderId)
        {
            return Ok(await _ordersService.RemoveAdressAsync(orderId));
        }
    }
}
