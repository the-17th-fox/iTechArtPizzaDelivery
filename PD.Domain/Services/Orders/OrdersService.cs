using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants.DeliveryMethods;
using PD.Domain.Constants.Exceptions;
using PD.Domain.Constants.OrderStatuses;
using PD.Domain.Constants.PaymentMethods;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IPizzasRepository _pizzasRepository;
        private readonly IPromoCodesRepository _promoCodesRepository;
        private readonly IMapper _mapper;
        public OrdersService(IOrdersRepository repository, IPizzasRepository pizzasRepository,
                                IPromoCodesRepository promoCodesRepository, IMapper mapper)
        {
            _ordersRepository = repository;
            _pizzasRepository = pizzasRepository;
            _promoCodesRepository = promoCodesRepository;
            _mapper = mapper;
        }

        public async Task<PageViewModel<ShortOrderViewModel>> GetAllAsync(PageSettingsViewModel pageSettings)
        {
            var orders = await _ordersRepository.GetAllAsync();

            var pagedList = PagedList<Order>.ToPagedList(orders, pageSettings.PageNumber, pageSettings.PageSize);

            return _mapper.Map<PagedList<Order>, PageViewModel<ShortOrderViewModel>>(pagedList);
        }

        public async Task<OrderViewModel> GetByIdAsync(long orderId)
        {
            var order = await GetAndCheckOrderAsync(orderId);

            var orderModel = _mapper.Map<OrderViewModel>(order);
            orderModel.TotalPrice = GetPriceWithDiscount(order);
            return orderModel;
        }

        public async Task<OrderViewModel> GetUsersActiveOrderAsync(long userId)
        {
            var order = await GetAndCheckActiveOrderByUserId(userId);

            var orderModel = _mapper.Map<OrderViewModel>(order);
            orderModel.TotalPrice = GetPriceWithDiscount(order);
            return orderModel;
        }

        public async Task<OrderViewModel> AddAsync(long userId)
        {
            // Checks if the user has any active order (last order)
            if (await _ordersRepository.GetUsersActiveOrderAsync(userId) != null)
                throw new BadRequestException("The user already has an active order.");

            var order = await _ordersRepository.AddAsync(userId);

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> AddPizzaAsync(long userId, long pizzaId, int numOfPizzasToAdd = 1)
        {
            var order = await GetAndCheckEditingReadyOrderAsync(userId);

            var pizza = await GetAndCheckPizzaAsync(pizzaId);

            if (order.Pizzas.Contains(pizza))
                order = await _ordersRepository.AddIncludedPizza(order, pizza, numOfPizzasToAdd);
            else
                order = await _ordersRepository.AddNotIncludedPizzaAsync(order, pizza, numOfPizzasToAdd);

            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> RemovePizzaAsync(long userId, long pizzaId, int numOfPizzasToRemove = 1)
        {
            var order = await GetAndCheckEditingReadyOrderAsync(userId);

            var pizza = await GetAndCheckPizzaAsync(pizzaId);

            if (!order.Pizzas.Contains(pizza))
                throw new NotFoundException("The specified order does not contain the specified pizza.");

            if (GetSpecifiedPizzaAmount(order, pizza) < numOfPizzasToRemove)
                throw new BadRequestException("The number of pizzas to remove is greater than pizzas amount in the order.");

            order = await _ordersRepository.RemovePizzaAsync(order, pizza);

            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<ShortOrderViewModel> DeleteActiveOrderAsync(long userId)
        {
            var order = await GetAndCheckEditingReadyOrderAsync(userId);

            await _ordersRepository.DeleteAsync(order);

            return _mapper.Map<ShortOrderViewModel>(order);
        }

        public async Task<ShortOrderViewModel> DeleteAnyAsync(long orderId)
        {
            var order = await GetAndCheckOrderAsync(orderId);

            await _ordersRepository.DeleteAsync(order);

            return _mapper.Map<ShortOrderViewModel>(order);
        }

        public async Task<OrderAdressViewModel> UpdateAdressAsync(long userId, string adress)
        {
            var order = await GetAndCheckEditingReadyOrderAsync(userId);

            await _ordersRepository.UpdateAdressAsync(order, adress);

            return _mapper.Map<OrderAdressViewModel>(order);
        }

        public async Task<OrderDeliveryMethodViewModel> UpdateDeliveryMethodAsync(long userId, int methodId)
        {
            var order = await GetAndCheckEditingReadyOrderAsync(userId);

            if (!IsDeliveryMethodExists(methodId))
                throw new BadRequestException("There is no delivery method with the specified Id.");

            await _ordersRepository.UpdateDeliveryMethodAsync(order, methodId);
            
            return _mapper.Map<OrderDeliveryMethodViewModel>(order);
        }

        public async Task<OrderStatusViewModel> UpdateOrderStatusAsync(long orderId, int statusId)
        {
            var order = await GetAndCheckActiveOrderByIdAsync(orderId);

            if (!IsOrderStatusExists(statusId))
                throw new NotFoundException("There is no order status with the specified Id.");

            await _ordersRepository.UpdateOrderStatusAsync(order, statusId);

            return _mapper.Map<OrderStatusViewModel>(order);
        }

        public async Task<OrderDescriptionViewModel> UpdateDescriptionAsync(long userId, string newDescription)
        {
            var order = await GetAndCheckEditingReadyOrderAsync(userId);

            await _ordersRepository.UpdateDescriptionAsync(order, newDescription);

            return _mapper.Map<OrderDescriptionViewModel>(order);
        }

        public async Task<OrderPromoCodeViewModel> UpdatePromoCodeAsync(long userId, string promoCodeName)
        {
            var order = await GetAndCheckEditingReadyOrderAsync(userId);

            var promoCode = await _promoCodesRepository.GetByNameAsync(promoCodeName);
            // Checks if the promocode is expired
            if (DateTime.Compare(promoCode.ExpirationDate, DateTime.UtcNow) < 0)
                throw new BadRequestException("The promocode is expired.");

            await _ordersRepository.UpdatePromoCodeAsync(order, promoCode);

            return _mapper.Map<OrderPromoCodeViewModel>(order);
        }

        public async Task<OrderIsActiveStatusViewModel> UpdateIsActiveStatusAsync(long orderId, bool status)
        {
            var order = await GetAndCheckOrderAsync(orderId);

            await _ordersRepository.UpdateIsActiveStatusAsync(order, status);

            return _mapper.Map<OrderIsActiveStatusViewModel>(order);
        }

        public bool IsDeliveryMethodExists(int methodId)
        {
            return Enum.IsDefined(typeof(DeliveryMethods), methodId);
        }

        public bool IsOrderStatusExists(int statusId)
        {
            return Enum.IsDefined(typeof(OrderStatuses), statusId);
        }

        public int GetSpecifiedPizzaAmount(Order order, Pizza pizza)
        {
            return order.PizzasInOrders.Find(po => po.PizzaId == pizza.Id).Amount;
        }

        public float GetPriceWithDiscount(Order order)
        {
            var pizzasPrice = order.PizzasInOrders
                .Where(po => po.Order == order)
                .Select(po => po.Pizza.Price * po.Amount)
                .Sum();

            var promoCodeDiscount = order.PromoCode == null ? 0 : order.PromoCode.DiscountAmount;

            var discount = (pizzasPrice * promoCodeDiscount) / 100;

            return pizzasPrice - discount;
        }

        public async Task<Order> GetAndCheckOrderAsync(long orderId)
        {
            var order = await _ordersRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException("The order with the specified id does not exist.");
            return order;
        }

        public async Task<Order> GetAndCheckEditingReadyOrderAsync(long userId)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            if (order == null || order.IsActive == false)
                throw new NotFoundException("The user does not have an active order or an order can not be edited anymore.");
            return order;
        }

        public async Task<Order> GetAndCheckActiveOrderByIdAsync(long orderId)
        {
            var order = await _ordersRepository.GetByIdAsync(orderId);
            if (order == null || order.IsActive == false)
                throw new NotFoundException("The order is not active anymore or it does not exist.");
            return order;
        }

        public async Task<Order> GetAndCheckActiveOrderByUserId(long userId)
        {
            var order = await _ordersRepository.GetUsersActiveOrderAsync(userId);
            if (order == null || order.IsActive == false)
                throw new NotFoundException("The order is not active anymore or it does not exist.");
            return order;
        }

        public async Task<Pizza> GetAndCheckPizzaAsync(long pizzaId)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(pizzaId);
            if(pizza == null)
                throw new NotFoundException("The pizza with the specified id does not exist.");
            return pizza;
        }
    }
}
