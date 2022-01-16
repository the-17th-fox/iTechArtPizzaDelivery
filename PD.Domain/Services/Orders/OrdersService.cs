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
            // Checks if there are any orders in the database
            if (orders.IsNullOrEmpty())
                throw new NotFoundException("No orders were found.");

            var pagedList = PagedList<Order>.ToPagedList(orders, pageSettings.PageNumber, pageSettings.PageSize);

            return _mapper.Map<PagedList<Order>, PageViewModel<ShortOrderViewModel>>(pagedList);
        }

        public async Task<OrderViewModel> GetByIdAsync(long orderId)
        {
            var order = await _ordersRepository.GetByIdAsync(orderId);
            // Checks if there is any order with the specified ID    
            if (order == null)
                throw new NotFoundException("The order was not found.");

            var orderModel = _mapper.Map<OrderViewModel>(order);
            orderModel.TotalPrice = GetPriceWithDiscount(order);
            return orderModel;
        }

        public async Task<OrderViewModel> GetUsersActiveOrderAsync(long userId)
        {
            var order = await _ordersRepository.GetUsersActiveOrderAsync(userId);
            // Checks if the user has any active order
            if (order == null)
                throw new NotFoundException("The user has no orders.");

            var orderModel = _mapper.Map<OrderViewModel>(order);
            orderModel.TotalPrice = GetPriceWithDiscount(order);
            return orderModel;
        }

        public async Task<OrderViewModel> AddAsync(long userId)
        {
            // Checks if the user has any active order (last order)
            if (await _ordersRepository.GetUsersActiveOrderAsync(userId) != null)
                throw new BadRequestException("The user already has an active order.");

            var order = new Order()
            {
                UserId = userId,
                OrderStatusId = (int)OrderStatuses.InProccesOfCreating,
                DeliveryMethodId = (int)DeliveryMethods.Delivery,
                PaymentMethodId = (int)PaymentMethods.Cash,
                PizzasInOrders = new List<PizzaOrder>()
            };

            await _ordersRepository.AddAsync(order);

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> AddPizzaAsync(long userId, long pizzaId, int numOfPizzasToAdd = 1)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited 
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

            var pizza = await _pizzasRepository.GetByIdAsync(pizzaId);
            // Checks if the pizza exists
            if (pizza == null)
                throw new BadRequestException("The pizza with the specified id does not exist.");

            await _ordersRepository.AddPizzaAsync(order, pizza, numOfPizzasToAdd);

            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> RemovePizzaAsync(long userId, long pizzaId, int numOfPizzasToRemove = 1)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new NotFoundException("The user does not have an active order.");

            var pizza = await _pizzasRepository.GetByIdAsync(pizzaId);
            // Checks if the pizza exists
            if (pizza == null)// Checks whether the deletion was successfull
                throw new BadRequestException("The pizza with the specified id does not exist.");

            if (!order.Pizzas.Contains(pizza))
                throw new BadRequestException("The specified order does not contain the specified pizza.");

            if (_ordersRepository.GetSpecifiedPizzaAmount(order, pizza) < numOfPizzasToRemove)
                throw new BadRequestException("The number of pizzas to remove is greater than pizzas amount in the order");

            await _ordersRepository.RemovePizzaAsync(order, pizza);

            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<string> DeleteActiveOrderAsync(long userId)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

            await _ordersRepository.DeleteAsync(order);

            return "Order has been deleted successfully.";
        }

        public async Task<string> DeleteAnyAsync(long orderId)
        {
            var order = await _ordersRepository.GetByIdAsync(orderId);
            // Checks if there is any order with the specified ID    
            if (order == null)
                throw new NotFoundException("The order was not found.");

            await _ordersRepository.DeleteAsync(order);

            return "Order has been deleted successfully.";
        }

        public async Task<OrderAdressViewModel> UpdateAdressAsync(long userId, string adress)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

            await _ordersRepository.UpdateAdressAsync(order, adress);

            return _mapper.Map<OrderAdressViewModel>(order);
        }

        public async Task<OrderDeliveryMethodViewModel> UpdateDeliveryMethodAsync(long userId, int methodId)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

            if (!IsDeliveryMethodExists(methodId))
                throw new BadRequestException("There is no delivery method with the specified Id.");

            await _ordersRepository.UpdateDeliveryMethodAsync(order, methodId);
            
            return _mapper.Map<OrderDeliveryMethodViewModel>(order);
        }

        public async Task<OrderStatusViewModel> UpdateOrderStatusAsync(long userId, int statusId)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

            if (IsOrderStatusExists(statusId))
                throw new BadRequestException("There is no order status with the specified Id.");

            await _ordersRepository.UpdateOrderStatusAsync(order, statusId);

            return _mapper.Map<OrderStatusViewModel>(order);
        }

        public async Task<OrderDescriptionViewModel> UpdateDescriptionAsync(long userId, string newDescription)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

            await _ordersRepository.UpdateDescriptionAsync(order, newDescription);

            return _mapper.Map<OrderDescriptionViewModel>(order);
        }

        public async Task<OrderPromoCodeViewModel> UpdatePromoCodeAsync(long userId, string promoCodeName)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

            var promoCode = await _promoCodesRepository.GetByNameAsync(promoCodeName);
            // Checks if the promocode is expired
            if (DateTime.Compare(promoCode.ExpirationDate, DateTime.UtcNow) < 0)
                throw new BadRequestException("The promocode is expired.");

            await _ordersRepository.UpdatePromoCodeAsync(order, promoCode);

            return _mapper.Map<OrderPromoCodeViewModel>(order);
        }

        public async Task<OrderIsActiveStatusViewModel> UpdateIsActiveStatusAsync(long userId, bool status)
        {
            var order = await _ordersRepository.GetEditingReadyAsync(userId);
            // Checks if the user has any order that can be edited
            if (order == null)
                throw new BadRequestException("The user does not have an active order.");

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

        public float GetPriceWithDiscount(Order order)
        {
            var pizzasPrice = order.PizzasInOrders
                .Where(po => po.Order == order)
                .Select(po => po.Pizza.Price * po.Amount)
                .Sum();
                

            var discount = (pizzasPrice * order.PromoCode.DiscountAmount) / 100;

            return pizzasPrice - discount;
        }
    }
}
