using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IOrdersService
    {
        public PageViewModel<ShortOrderViewModel> GetAllAsync(PageSettingsViewModel pageSettings);
        public Task<OrderViewModel> GetByIdAsync(long orderId);
        public Task<OrderViewModel> GetUsersActiveOrderAsync(long userId);

        public float GetPriceWithDiscount(Order order);

        public Task<OrderViewModel> AddAsync(long userId);
        public Task<ShortOrderViewModel> DeleteActiveOrderAsync(long userId);
        public Task<ShortOrderViewModel> DeleteAnyAsync(long orderId);

        public Task<OrderIsActiveStatusViewModel> UpdateIsActiveStatusAsync(long orderId, bool status);
        public Task<OrderStatusViewModel> UpdateOrderStatusAsync(long orderId, int statusId);
        public Task<OrderDeliveryMethodViewModel> UpdateDeliveryMethodAsync(long userId, int methodId);
        public Task<OrderDescriptionViewModel> UpdateDescriptionAsync(long userId, string newDescription);
        public Task<OrderPromoCodeViewModel> UpdatePromoCodeAsync(long userId, string promoCodeName);
        public Task<OrderAdressViewModel> UpdateAdressAsync(long userId, string adress);

        public Task<OrderPizzasViewModel> AddPizzaAsync(long userId, long pizzaId, int numOfPizzasToAdd = 1);
        public Task<OrderPizzasViewModel> RemovePizzaAsync(long userId, long pizzaId, int numOfPizzasToRemove = 1);

        public bool IsDeliveryMethodExists(int methodId);
        public bool IsOrderStatusExists(int statusId);
        public int GetSpecifiedPizzaAmount(Order order, Pizza pizza);

        public Task<Order> GetAndCheckOrderAsync(long orderId);
        public Task<Order> GetAndCheckEditingReadyOrderAsync(long userId);
        public Task<Order> GetAndCheckActiveOrderByIdAsync(long orderId);
        public Task<Order> GetAndCheckActiveOrderByUserId(long userId);
        public Task<Pizza> GetAndCheckPizzaAsync(long pizzaId);
    }
}
