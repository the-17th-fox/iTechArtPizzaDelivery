using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Web.Models;
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
        private readonly IMapper _mapper;
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService service, IMapper mapper)
        {
            _ordersService = service;
            _mapper = mapper;
        }

        [Route("all")]
        [ActionName(nameof(GetAllAsync))]
        [HttpGet]
        public async Task<List<ShortOrderViewModel>> GetAllAsync()
        {
            List<Order> orders = await _ordersService.GetAllAsync();
            return _mapper.Map<List<ShortOrderViewModel>>(orders);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<OrderViewModel> GetByIdAsync(long id)
        {
            Order order = await _ordersService.GetByIdAsync(id);
            return _mapper.Map<OrderViewModel>(order);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ShortOrderViewModel> AddAsync(AddOrderViewModel orderModel)
        {
            Order newOrder = _mapper.Map<AddOrderViewModel, Order>(orderModel);
            await _ordersService.AddAsync(newOrder);
            return _mapper.Map<ShortOrderViewModel>(newOrder);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<ShortOrderViewModel> DeleteAsync(long id)
        {
            Order orderToRemove = await _ordersService.DeleteAsync(id);
            return _mapper.Map<ShortOrderViewModel>(orderToRemove);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<PizzasInOrderViewModel> AddPizzaToOrderAsync(long pizzaId, long orderId)
        {
            Order order = await _ordersService.AddPizzaToOrderAsync(pizzaId, orderId);
            return _mapper.Map<PizzasInOrderViewModel>(order);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<PizzasInOrderViewModel> RemovePizzaFromOrderAsync(long pizzaId, long orderId)
        {
            Order order = await _ordersService.RemovePizzaFromOrderAsync(pizzaId, orderId);
            return _mapper.Map<PizzasInOrderViewModel>(order);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<PromoCodeInOrderViewModel> AddPromoCodeToOrderAsync(long promoCodeId, long orderId)
        {
            Order order = await _ordersService.AddPromoCodeToOrderAsync(promoCodeId, orderId);
            return _mapper.Map<PromoCodeInOrderViewModel>(order);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<PromoCodeInOrderViewModel> RemovePromoCodeFromOrderAsync(long orderId)
        {
            Order order = await _ordersService.RemovePromoCodeFromOrderAsync(orderId);
            return _mapper.Map<PromoCodeInOrderViewModel>(order);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<AdressInOrderViewModel> AddAdressToOrderAsync(string adress, long orderId)
        {
            Order order = await _ordersService.AddAdressToOrderAsync(adress, orderId);
            return _mapper.Map<AdressInOrderViewModel>(order);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<AdressInOrderViewModel> RemoveAdressFromOrderAsync(long orderId)
        {
            Order order = await _ordersService.RemoveAdressFromOrderAsync(orderId);
            return _mapper.Map<AdressInOrderViewModel>(order);
        }
    }
}
