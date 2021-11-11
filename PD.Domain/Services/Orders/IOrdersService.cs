using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IOrdersService
    {
        public Task<List<Order>> GetAllAsync();
        public Task<Order> GetByIdAsync(int id);
        public Task<Order> AddAsync(int userId, string adress, int? promoCodeId = null);
        public Task<Order> AddPizzaToOrder(int pizzaId, int orderId);
        public Task<Order> DeleteAsync(int id);
    }
}
