using Microsoft.EntityFrameworkCore;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class OrdersEFRepository : IOrdersRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public OrdersEFRepository(PizzaDeliveryContext context) => _dbContext = context;

        public async Task<Order> AddAsync(int userId, int pizzaId, string adress, int? promoCodeId = null)
        {
            var newOrder = _dbContext.Orders
                .Add(new Order
            {
                UserID = userId,
                PizzaID = pizzaId,
                Adress = adress,
                PromoCodeID = promoCodeId
            });

            await _dbContext.SaveChangesAsync();
            return newOrder.Entity;
        }

        public async Task<Order> DeleteAsync(int id)
        {
            Order orderToRemove = await _dbContext.Orders.FindAsync(id);
            _dbContext.Orders.Remove(orderToRemove);

            await _dbContext.SaveChangesAsync();
            return orderToRemove;
        }

        public async Task<Order> GetByIdAsync(int id) => await _dbContext.Orders.FindAsync(id);

        public async Task<List<Order>> GetAllAsync() => await _dbContext.Orders.ToListAsync();
    }
}
