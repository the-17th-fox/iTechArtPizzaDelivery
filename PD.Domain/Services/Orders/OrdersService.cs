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

        public async Task<Order> AddAsync(int userId) 
            => await _repository.AddAsync(userId);

        public async Task<Order> DeleteAsync(int id) 
            => await _repository.DeleteAsync(id);
        public async Task<Order> GetByIdAsync(int id) 
            => await _repository.GetByIdAsync(id);

        public async Task<List<Order>> GetAllAsync() 
            => await _repository.GetAllAsync();


        public async Task<Order> AddPizzaToOrderAsync(int pizzaId, int orderId)
            => await _repository.AddPizzaToOrderAsync(pizzaId, orderId);

        public async Task<Order> RemovePizzaFromOrderAsync(int pizzaId, int orderId)
            => await _repository.RemovePromoCodeFromOrderAsync(pizzaId, orderId);

        public async Task<Order> AddPromoCodeToOrderAsync(int promoCodeId, int orderId)
            => await _repository.AddPromoCodeToOrderAsync(promoCodeId, orderId);

        public async Task<Order> RemovePromoCodeFromOrderAsync(int promoCodeId, int orderId)
            => await _repository.RemovePromoCodeFromOrderAsync(promoCodeId, orderId);

        public async Task<Order> AddAdressToOrderAsync(string adress, int orderId)
            => await _repository.AddAdressToOrderAsync(adress, orderId);

        public async Task<Order> RemoveAdressFromOrderAsync(string adress, int orderId)
            => await _repository.RemoveAdressFromOrderAsync(adress, orderId);
    }
}
