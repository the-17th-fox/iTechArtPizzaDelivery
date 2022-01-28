using Microsoft.EntityFrameworkCore;
using PD.Domain.Constants.DeliveryMethods;
using PD.Domain.Constants.Exceptions;
using PD.Domain.Constants.OrderStatuses;
using PD.Domain.Constants.PaymentMethods;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;
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

        public async Task<Order> AddAsync(long userId)
        {
            try
            {
                var order = new Order()
                {
                    UserId = userId,
                    OrderStatusId = (int)OrderStatuses.InProccesOfCreating,
                    DeliveryMethodId = (int)DeliveryMethods.Delivery,
                    PaymentMethodId = (int)PaymentMethods.Cash,
                    PizzasInOrders = new List<PizzaOrder>()
                };

                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateException)
            {
                throw new CreatingFailedException();
            }
        }

        public async Task<Order> AddNotIncludedPizzaAsync(Order order, Pizza pizza, int numOfPizzasToAdd = 1)
        {
            try
            {
                order.PizzasInOrders.Add(
                    new PizzaOrder 
                    { 
                        Order = order, 
                        OrderId = order.Id, 
                        Pizza = pizza, 
                        PizzaId = pizza.Id, 
                        Amount = numOfPizzasToAdd 
                    });

                await _dbContext.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task<Order> AddIncludedPizza(Order order, Pizza pizza, int numOfPizzasToAdd = 1)
        {
            try
            {
                order.PizzasInOrders
                    .Find(po => po.PizzaId == pizza.Id)
                    .Amount += numOfPizzasToAdd;

                await _dbContext.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task<Order> RemovePizzaAsync(Order order, Pizza pizza, int numOfPizzasToRemove = 1)
        {
            try
            {
                order.PizzasInOrders
                    .Find(po => po.Pizza == pizza)
                    .Amount -= numOfPizzasToRemove;

                await _dbContext.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task DeleteAsync(Order order)
        {
            try
            {
                _dbContext.Orders.Remove(order);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DeletionFailedException();
            }
        }

        public async Task<Order> GetByIdAsync(long id)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Pizzas)
                .Include(o => o.PizzasInOrders)
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public PagedList<Order> GetAllAsync(PageSettingsViewModel pageSettings)
        {
            IQueryable<Order> ordersIQuer = _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Pizzas);

            return PagedList<Order>.ToPagedList(ordersIQuer, pageSettings.PageNumber, pageSettings.PageSize);
        }

        public async Task UpdatePromoCodeAsync(Order order, PromoCode promoCode)
        {
            try
            {
                order.PromoCode = promoCode;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task<Order> UpdateOrderStatusAsync(Order order, int statusId)
        {
            try
            {
                order.OrderStatusId = statusId;
                await _dbContext.SaveChangesAsync();
                return order;
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task UpdateDeliveryMethodAsync(Order order, int methodId)
        {
            try
            {
                order.DeliveryMethodId = methodId;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task UpdateDescriptionAsync(Order order, string newDescription)
        {
            try
            {
                order.Description = newDescription;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task UpdateAdressAsync(Order order, string adress)
        {
            try
            {
                order.Adress = adress;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task<Order> GetUsersActiveOrderAsync(long userId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Pizzas)
                .Include(o => o.PromoCode)
                .Where(o => o.UserId == userId)
                .Where(o => o.IsActive == true)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetEditingReadyAsync(long userId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Include(o => o.Pizzas)
                .Include(o => o.PizzasInOrders)
                .Where(o => o.UserId == userId)
                .Where(o => o.IsActive == true)
                .Where(o => o.OrderStatusId == (int)OrderStatuses.InProccesOfCreating)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateIsActiveStatusAsync(Order order, bool status)
        {
            try
            {
                order.IsActive = status;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task<List<Order>> GetAllFromUserAsync(long userId)
        {
            return await _dbContext.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
