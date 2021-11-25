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

        public async Task<Order> AddPizzaToOrderAsync(long pizzaId, long orderId)
        {
            return await _repository.AddPizzaToOrderAsync(pizzaId, orderId);
        }

        public async Task<Order> RemovePizzaFromOrderAsync(long pizzaId, long orderId)
        {
            return await _repository.RemovePizzaFromOrderAsync(pizzaId, orderId);
        }

        public async Task<Order> AddPromoCodeToOrderAsync(long promoCodeId, long orderId)
        {
            return await _repository.AddPromoCodeToOrderAsync(promoCodeId, orderId);
        }

        public async Task<Order> RemovePromoCodeFromOrderAsync(long orderId)
        {
            return await _repository.RemovePromoCodeFromOrderAsync(orderId);
        }

        public async Task<Order> AddAdressToOrderAsync(string adress, long orderId)
        {
            return await _repository.AddAdressToOrderAsync(adress, orderId);
        }

        public async Task<Order> RemoveAdressFromOrderAsync(long orderId)
        {
            return await _repository.RemoveAdressFromOrderAsync(orderId);
        }

        public async Task<List<Order>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Order> GetByIdAsync(long id) => await _repository.GetByIdAsync(id);

        public async Task<Order> AddAsync(Order entity) => await _repository.AddAsync(entity);

        public async Task<Order> DeleteAsync(long id) => await _repository.DeleteAsync(id);
    }
}
