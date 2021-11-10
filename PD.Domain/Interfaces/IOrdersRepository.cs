using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PD.Domain.Interfaces
{
    public interface IOrdersRepository
    {
        public Task<List<Order>> GetOrdersAsync();
        public Task<Order> GetOrderAsync(int id);
        public Task<Order> AddOrderAsync(int userId, int pizzaId, string adress, int? promoCodeId = null);
        public Task<Order> DeleteOrderAsync(int id);
    }
}
