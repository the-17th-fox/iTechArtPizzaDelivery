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
        public Task<List<ShortOrderViewModel>> GetAllAsync();
        public Task<OrderViewModel> GetByIdAsync(long id);
        public Task<OrderViewModel> AddAsync(AddOrderViewModel model);
        public Task<OrderViewModel> DeleteAsync(long id);
        public Task<OrderIsPaidStatusViewModel> ChangeIsPaidStatusAsync(int orderId, bool isPaid);
        public Task<OrderDeliveryStatusViewModel> ChangeDeliveryStatusAsync(int orderId, string status);
        public Task<OrderDeliveryMethodViewModel> ChangeDeliveryMethodAsync(int orderId, string method);
        public Task<OrderDescriptionViewModel> ChangeDescriptionAsync(int orderId, string newDescription);

        public Task<OrderPizzasViewModel> AddPizzaAsync(long pizzaId, long orderId);
        public Task<OrderPizzasViewModel> RemovePizzaAsync(long pizzaId, long orderId);

        public Task<OrderPromoCodeViewModel> AddPromoCodeAsync(long promoCodeId, long orderId);
        public Task<OrderPromoCodeViewModel> RemovePromoCodeAsync(long orderId);

        public Task<OrderAdressViewModel> AddAdressAsync(string adress, long orderId);
        public Task<OrderAdressViewModel> RemoveAdressAsync(long orderId);
    }
}
