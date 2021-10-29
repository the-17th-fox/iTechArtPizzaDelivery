using iTechArtPizzaDelivery.Domain.Entities;
using iTechArtPizzaDelivery.Domain.Interfaces;
using iTechArtPizzaDelivery.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class PizzasEFRepository : IPizzasRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public PizzasEFRepository(PizzaDeliveryContext context) => this._dbContext = context;


        public async Task<List<Pizza>> GetPizzasAsync()
        {
            return await _dbContext.Pizzas.ToListAsync();
        }

        public async Task<Pizza> GetPizzaByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Pizzas
                .FirstAsync(p => p.PizzaID == id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Pizza> CreatePizzaAsync(Pizza pizza)
        {
            var newPizza = await _dbContext.Pizzas.AddAsync(pizza);
            await _dbContext.SaveChangesAsync();
            return newPizza.Entity;
        }
    }
}
