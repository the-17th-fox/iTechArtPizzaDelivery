using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IOrdersService : IBaseService<Order>
    {
        // Unique methods here
        public Task<Order> AddPizzaToOrderAsync(long pizzaId, long orderId);
        public Task<Order> RemovePizzaFromOrderAsync(long pizzaId, long orderId);

        public Task<Order> AddPromoCodeToOrderAsync(long promoCodeId, long orderId);
        public Task<Order> RemovePromoCodeFromOrderAsync(long orderId);

        public Task<Order> AddAdressToOrderAsync(string adress, long orderId);
        public Task<Order> RemoveAdressFromOrderAsync(long orderId);
    }
}
