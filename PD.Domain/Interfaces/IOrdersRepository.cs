using PD.Domain.Entities;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PD.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        public Task<List<Order>> GetAllAsync();
        public Task<Order> GetByIdAsync(long id);
        public Task<Order> AddAsync(AddOrderViewModel model);
        public Task<Order> DeleteAsync(long id);
        public Task<Order> ChangeIsPaidStatusAsync(long orderId, bool isPaid);
        public Task<Order> ChangeDeliveryStatusAsync(long orderId, string status);
        public Task<Order> ChangeDeliveryMethodAsync(long orderId, string method);
        public Task<Order> ChangeDescriptionAsync(long orderId, string newDescription);

        public Task<Order> AddPizzaAsync(long pizzaId, long orderId);
        public Task<Order> RemovePizzaAsync(long pizzaId, long orderId);

        public Task<Order> AddPromoCodeAsync(long promoCodeId, long orderId);
        public Task<Order> RemovePromoCodeAsync(long orderId);

        public Task<Order> AddAdressAsync(string adress, long orderId);
        public Task<Order> RemoveAdressAsync(long orderId);
    }
}
