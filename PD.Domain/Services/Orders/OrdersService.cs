using AutoMapper;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
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
        private readonly IMapper _mapper;
        public OrdersService(IOrdersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ShortOrderViewModel>> GetAllAsync()
        {
            List<Order> orders = await _repository.GetAllAsync();
            return _mapper.Map<List<ShortOrderViewModel>>(orders);
        }

        public async Task<OrderViewModel> GetByIdAsync(long id)
        {
            Order order = await _repository.GetByIdAsync(id);
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderViewModel> AddAsync(AddOrderViewModel model)
        {
            Order order = await _repository.AddAsync(model);
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderViewModel> DeleteAsync(long id)
        {
            Order order = await _repository.DeleteAsync(id);
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task<OrderIsPaidStatusViewModel> ChangeIsPaidStatusAsync(int orderId, bool isPaid)
        {
            Order order = await _repository.ChangeIsPaidStatusAsync(orderId, isPaid);
            return _mapper.Map<OrderIsPaidStatusViewModel>(order);
        }

        public async Task<OrderDeliveryStatusViewModel> ChangeDeliveryStatusAsync(int orderId, string status)
        {
            Order order = await _repository.ChangeDeliveryStatusAsync(orderId, status);
            return _mapper.Map<OrderDeliveryStatusViewModel>(order);
        }

        public async Task<OrderDeliveryMethodViewModel> ChangeDeliveryMethodAsync(int orderId, string method)
        {
            Order order = await _repository.ChangeDeliveryMethodAsync(orderId, method);
            return _mapper.Map<OrderDeliveryMethodViewModel>(order);
        }

        public async Task<OrderDescriptionViewModel> ChangeDescriptionAsync(int orderId, string newDescription)
        {
            Order order = await _repository.ChangeDescriptionAsync(orderId, newDescription);
            return _mapper.Map<OrderDescriptionViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> AddPizzaAsync(long pizzaId, long orderId)
        {
            Order order = await _repository.AddPizzaAsync(pizzaId, orderId);
            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<OrderPizzasViewModel> RemovePizzaAsync(long pizzaId, long orderId)
        {
            Order order = await _repository.RemovePizzaAsync(pizzaId, orderId);
            return _mapper.Map<OrderPizzasViewModel>(order);
        }

        public async Task<OrderPromoCodeViewModel> AddPromoCodeAsync(long promoCodeId, long orderId)
        {
            Order order = await _repository.AddPromoCodeAsync(promoCodeId, orderId);
            return _mapper.Map<OrderPromoCodeViewModel>(order);
        }

        public async Task<OrderPromoCodeViewModel> RemovePromoCodeAsync(long orderId)
        {
            Order order = await _repository.RemovePromoCodeAsync(orderId);
            return _mapper.Map<OrderPromoCodeViewModel>(order);
        }

        public async Task<OrderAdressViewModel> AddAdressAsync(string adress, long orderId)
        {
            Order order = await _repository.AddAdressAsync(adress, orderId);
            return _mapper.Map<OrderAdressViewModel>(order);
        }

        public async Task<OrderAdressViewModel> RemoveAdressAsync(long orderId)
        {
            Order order = await _repository.RemoveAdressAsync(orderId);
            return _mapper.Map<OrderAdressViewModel>(order);
        }
    }
}
