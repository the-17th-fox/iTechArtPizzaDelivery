﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<Order> AddAsync(Order order)
        {
            var newOrder = _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();
            return newOrder.Entity;
        }

        public async Task<Order> AddPizzaToOrderAsync(long pizzaId, long orderId)
        {
            Pizza pizza = await _dbContext.Pizzas
                .Include(i => i.Orders)
                .Where(i => i.Id == pizzaId)
                .FirstAsync();

            Order order = await _dbContext.Orders
                .Include(i => i.Pizzas)
                .Where(i => i.Id == orderId)
                .FirstAsync();

            order.Pizzas.Add(pizza);

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> RemovePizzaFromOrderAsync(long pizzaId, long orderId)
        {
            Pizza pizza = await _dbContext.Pizzas
                .Include(i => i.Orders)
                .Where(i => i.Id == pizzaId)
                .FirstAsync();

            Order order = await _dbContext.Orders
                .Include(i => i.Pizzas)
                .Where(i => i.Id == orderId)
                .FirstAsync();

            order.Pizzas.Remove(pizza);

            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteAsync(long id)
        {
            Order orderToRemove = await _dbContext.Orders.FindAsync(id);
            _dbContext.Orders.Remove(orderToRemove);

            await _dbContext.SaveChangesAsync();
            return orderToRemove;
        }

        public async Task<Order> GetByIdAsync(long id)
        {
            return await _dbContext.Orders
                .Include(o => o.Pizzas)
                .Where(o => o.Id == id)
                .FirstAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _dbContext.Orders
                .Include(o => o.Pizzas)
                .ToListAsync();
        }

        public async Task<Order> AddPromoCodeToOrderAsync(long promoCodeId, long orderId)
        { 
            Order order = await _dbContext.Orders.FindAsync(orderId);

            PromoCode promoCode = await _dbContext.PromoCodes.FindAsync(promoCodeId);

            order.PromoCode = promoCode;
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> RemovePromoCodeFromOrderAsync(long orderId)
        {   
            Order order = await _dbContext.Orders.FindAsync(orderId);

            PromoCode promoCode = order.PromoCode;

            order.PromoCodeId = null;
            promoCode.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> AddAdressToOrderAsync(string adress, long orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Adress = adress;
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order> RemoveAdressFromOrderAsync(long orderId)
        {
            Order order = await _dbContext.Orders.FindAsync(orderId);

            order.Adress = null;
            await _dbContext.SaveChangesAsync();

            return order;
        }
    }
}
