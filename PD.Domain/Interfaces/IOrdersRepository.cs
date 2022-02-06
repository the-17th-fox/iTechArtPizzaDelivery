using PD.Domain.Entities;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PD.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        public PagedList<Order> GetAllAsync(PageSettingsViewModel pageSettings);
        public Task<Order> GetByIdWithoutTrackingAsync(long id);
        public Task<Order> GetByIdAsync(long id);
        public Task<Order> GetActiveOrderAsync(long userId);
        public Task<Order> GetEditingReadyAsync(long userId);
        public Task<List<Order>> GetAllFromUserWithoutTrackingAsync(long userId);

        public Task<Order> AddAsync(long userId);
        public Task DeleteAsync(Order order);

        public Task UpdateIsActiveStatusAsync(Order order, bool status);
        public Task<Order> UpdateOrderStatusAsync(Order order, int statusId);
        public Task UpdateDeliveryMethodAsync(Order order, int methodId);
        public Task UpdateDescriptionAsync(Order order, string newDescription);
        public Task UpdatePromoCodeAsync(Order order, PromoCode promoCode);
        public Task UpdateAdressAsync(Order order, string adress);

        public Task<Order> AddNotIncludedPizzaAsync(Order order, Pizza pizza, int numOfPizzasToAdd = 1);
        public Task<Order> AddIncludedPizza(Order order, Pizza pizza, int numOfPizzasToAdd = 1);
        public Task<Order> RemovePizzaAsync(Order order, Pizza pizza, int numOfPizzasToRemove = 1);
        public Task<Order> RemoveAllPizzasOfType(Order order, Pizza pizza);
    }
}
