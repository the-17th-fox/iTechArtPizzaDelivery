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

        public PageViewModel<ShortOrderViewModel> GetAllAsync(PageSettingsViewModel pageSettings)
        {
            var orders = _ordersRepository.GetAllAsync(pageSettings);

            return _mapper.Map<PagedList<Order>, PageViewModel<ShortOrderViewModel>>(orders);
        }

        public async Task<OrderViewModel> GetByIdAsync(long orderId)
        {
            var order = await GetAndCheckWithoutTrackingAsync(orderId);

            var orderModel = _mapper.Map<OrderViewModel>(order);
            orderModel.TotalPrice = GetPriceWithDiscount(order);
            return orderModel;
        }

        public async Task<OrderViewModel> GetUsersActiveOrderAsync(long userId)
        {
            var order = await GetAndCheckActiveByUserIdAsync(userId);

            var orderModel = _mapper.Map<OrderViewModel>(order);
            orderModel.TotalPrice = GetPriceWithDiscount(order);
            return orderModel;
        }

        public async Task<OrderViewModel> AddAsync(long userId)
        {
            // Checks if the user has any active order (last order)
            if (await _ordersRepository.GetActiveOrderAsync(userId) != null)
                throw new BadRequestException("The user already has an active order.");

            var order = await _ordersRepository.AddAsync(userId);

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> AddPizzaAsync(long userId, long pizzaId, int numOfPizzasToAdd = 1)
        {
            var order = await GetAndCheckEditingReadyAsync(userId);

            var pizza = await GetAndCheckPizzaAsync(pizzaId);

            if (order.Pizzas.Contains(pizza))
                order = await _ordersRepository.AddIncludedPizza(order, pizza, numOfPizzasToAdd);
            else
                order = await _ordersRepository.AddNotIncludedPizzaAsync(order, pizza, numOfPizzasToAdd);

            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> RemovePizzaAsync(long userId, long pizzaId, int numOfPizzasToRemove = 1)
        {
            var order = await GetAndCheckEditingReadyAsync(userId);

            var pizza = await GetAndCheckPizzaAsync(pizzaId);

            if (!order.Pizzas.Contains(pizza))
                throw new NotFoundException("The specified order does not contain the specified pizza.");

            if (GetSpecifiedPizzaAmount(order, pizza) < numOfPizzasToRemove)
                throw new BadRequestException("The number of pizzas to remove is greater than pizzas amount in the order.");


            if (GetSpecifiedPizzaAmount(order, pizza) == numOfPizzasToRemove)
                order = await _ordersRepository.RemoveAllPizzasOfType(order, pizza);
            else
                order = await _ordersRepository.RemovePizzaAsync(order, pizza, numOfPizzasToRemove);

            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<ShortOrderViewModel> DeleteActiveOrderAsync(long userId)
        {
            var order = await GetAndCheckEditingReadyAsync(userId);

            await _ordersRepository.DeleteAsync(order);

            return _mapper.Map<ShortOrderViewModel>(order);
        }

        public async Task<ShortOrderViewModel> DeleteAnyAsync(long orderId)
        {
            var order = await GetAndCheckAsync(orderId);

            await _ordersRepository.DeleteAsync(order);

            return _mapper.Map<ShortOrderViewModel>(order);
        }

        public async Task<OrderAdressViewModel> UpdateAdressAsync(long userId, string adress)
        {
            var order = await GetAndCheckEditingReadyAsync(userId);

            await _ordersRepository.UpdateAdressAsync(order, adress);

            return _mapper.Map<OrderAdressViewModel>(order);
        }

        public async Task<OrderDeliveryMethodViewModel> UpdateDeliveryMethodAsync(long userId, int methodId)
        {
            var order = await GetAndCheckEditingReadyAsync(userId);

            if (!IsDeliveryMethodExists(methodId))
                throw new BadRequestException("There is no delivery method with the specified Id.");

            await _ordersRepository.UpdateDeliveryMethodAsync(order, methodId);
            
            return _mapper.Map<OrderDeliveryMethodViewModel>(order);
        }

        public async Task<OrderStatusViewModel> UpdateOrderStatusAsync(long orderId, int statusId)
        {
            var order = await GetAndCheckActiveAsync(orderId);

            if (!IsOrderStatusExists(statusId))
                throw new NotFoundException("There is no order status with the specified Id.");

            await _ordersRepository.UpdateOrderStatusAsync(order, statusId);

            return _mapper.Map<OrderStatusViewModel>(order);
        }

        public async Task<OrderDescriptionViewModel> UpdateDescriptionAsync(long userId, string newDescription)
        {
            var order = await GetAndCheckEditingReadyAsync(userId);

            await _ordersRepository.UpdateDescriptionAsync(order, newDescription);

            return _mapper.Map<OrderDescriptionViewModel>(order);
        }

        public async Task<OrderPromoCodeViewModel> UpdatePromoCodeAsync(long userId, string promoCodeName)
        {
            var order = await GetAndCheckEditingReadyAsync(userId);

            var promoCode = await _promoCodesRepository.GetByNameAsync(promoCodeName);
            // Checks if the promocode is expired
            if (DateTime.Compare(promoCode.ExpirationDate, DateTime.UtcNow) < 0)
                throw new BadRequestException("The promocode is expired.");

            await _ordersRepository.UpdatePromoCodeAsync(order, promoCode);

            return _mapper.Map<OrderPromoCodeViewModel>(order);
        }

        public async Task<OrderIsActiveStatusViewModel> UpdateIsActiveStatusAsync(long orderId, bool status)
        {
            var order = await GetAndCheckAsync(orderId);

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
            if (order.Pizzas.Count == 0)
                return 0;

            var pizzasPrice = order.PizzasInOrders
                .Where(po => po.Order == order)
                .Select(po => po.Pizza.Price * po.Amount)
                .Sum();

            var promoCodeDiscount = order.PromoCode == null ? 0 : order.PromoCode.DiscountAmount;

            var discount = (pizzasPrice * promoCodeDiscount) / 100;

            return pizzasPrice - discount;
        }

        public async Task<Order> GetAndCheckAsync(long orderId)
        {
            var order = await _ordersRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException("The order with the specified id does not exist.");
            return order;
        }

        public async Task<Order> GetAndCheckWithoutTrackingAsync(long orderId)
        {
            var order = await _ordersRepository.GetByIdWithoutTrackingAsync(orderId);
            if (order == null)
                throw new NotFoundException("The order with the specified id does not exist.");
            return order;
        }

        public async Task<Order> GetAndCheckEditingReadyAsync(long userId)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            if (order == null || order.IsActive == false)
                throw new NotFoundException("The user does not have an active order or an order can not be edited anymore.");
            return order;
        }

        public async Task<Order> GetAndCheckActiveAsync(long orderId)
        {
            var order = await _ordersRepository.GetByIdAsync(orderId);
            if (order == null || order.IsActive == false)
                throw new NotFoundException("The order is not active anymore or it does not exist.");
            return order;
        }

        public async Task<Order> GetAndCheckActiveByUserIdAsync(long userId)
        {
            var order = await _ordersRepository.GetActiveOrderAsync(userId);
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
