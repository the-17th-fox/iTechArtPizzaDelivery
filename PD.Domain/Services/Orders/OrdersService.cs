using PD.Domain.Entities;
using PD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repository;
        public OrdersService(IOrdersRepository repository) => _repository = repository;

        public async Task<Order> AddOrderAsync(int userId, int pizzaId, string adress, int? promoCodeId = null)
            => await _repository.AddOrderAsync(userId, pizzaId, adress, promoCodeId);

        public async Task<Order> DeleteOrderAsync(int id) => await _repository.DeleteOrderAsync(id);

        public async Task<Order> GetOrderAsync(int id) => await _repository.GetOrderAsync(id);

        public async Task<List<Order>> GetOrdersAsync() => await _repository.GetOrdersAsync();
    }
}
