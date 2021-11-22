﻿using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PD.Domain.Interfaces
{
    public interface IOrdersRepository : IBaseRepository<Order>
    {
        public Task<Order> AddPizzaToOrderAsync(int pizzaId, int orderId);
        public Task<Order> RemovePizzaFromOrderAsync(int pizzaId, int orderId);

        public Task<Order> AddPromoCodeToOrderAsync(int promoCodeId, int orderId);
        public Task<Order> RemovePromoCodeFromOrderAsync(int orderId);

        public Task<Order> AddAdressToOrderAsync(string adress, int orderId);
        public Task<Order> RemoveAdressFromOrderAsync(int orderId);

        // TODO: Add status change
    }
}
