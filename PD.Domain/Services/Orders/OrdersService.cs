using PD.Domain.Entities;
using PD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repository;
        public OrdersService(IOrdersRepository repository) => _repository = repository;

        public async Task<Order> AddAsync(int userId, int pizzaId, string adress, int? promoCodeId = null)
            => await _repository.AddAsync(userId, pizzaId, adress, promoCodeId);

        public async Task<Order> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<Order> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<List<Order>> GetAllAsync() => await _repository.GetAllAsync();
    }
}
