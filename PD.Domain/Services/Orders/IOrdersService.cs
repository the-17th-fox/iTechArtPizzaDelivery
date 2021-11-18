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
        public Task<Order> AddPizzaToOrderAsync(int pizzaId, int orderId);
        public Task<Order> RemovePizzaFromOrderAsync(int pizzaId, int orderId);

        public Task<Order> AddPromoCodeToOrderAsync(int promoCodeId, int orderId);
        public Task<Order> RemovePromoCodeFromOrderAsync(int promoCodeId, int orderId);

        public Task<Order> AddAdressToOrderAsync(string adress, int orderId);
        public Task<Order> RemoveAdressFromOrderAsync(string adress, int orderId);
    }
}
