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

        public async Task<Order> AddAsync(int userId)
        {
            var newOrder = _dbContext.Orders
                .Add(new Order
                {
                    UserId = userId,
                    Pizzas = new List<Pizza>()
                });

            await _dbContext.SaveChangesAsync();
            return newOrder.Entity;
        }

        public async Task<Order> AddPizzaToOrder(int pizzaId, int orderId)
        {
            Pizza pizza = await _dbContext.Pizzas.FindAsync(pizzaId);
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Pizzas.Add(pizza);

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> RemovePizzaFromOrder(int pizzaId, int orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Pizzas.Remove(
                order.Pizzas.Find(p => p.Id == pizzaId)
                );

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteAsync(int id)
        {
            Order orderToRemove = await _dbContext.Orders.FindAsync(id);
            _dbContext.Orders.Remove(orderToRemove);

            await _dbContext.SaveChangesAsync();
            return orderToRemove;
        }

        public async Task<Order> GetByIdAsync(int id) 
            => await _dbContext.Orders.FindAsync(id);

        public async Task<List<Order>> GetAllAsync() 
            => await _dbContext.Orders.ToListAsync();

        public async Task<Order> AddPizzaToOrderAsync(int pizzaId, int orderId)
        {
            Pizza pizza = await _dbContext.Pizzas.FindAsync(pizzaId);
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Pizzas.Add(pizza);

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> RemovePizzaFromOrderAsync(int pizzaId, int orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Pizzas.Remove(
                order.Pizzas.Find(p => p.Id == pizzaId)
                );

            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> AddPromoCodeToOrderAsync(int promoCodeId, int orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.PromoCodeId = promoCodeId;
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> RemovePromoCodeFromOrderAsync(int promoCodeId, int orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.PromoCodeId = null;
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> AddAdressToOrderAsync(string adress, int orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Adress = adress;
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> RemoveAdressFromOrderAsync(string adress, int orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Adress = null;
            await _dbContext.SaveChangesAsync();

            return order;
        }
    }
}
