using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Domain.Models;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class PizzasEFRepository : IPizzasRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public PizzasEFRepository(PizzaDeliveryContext context) => _dbContext = context;

        public async Task<List<Pizza>> GetAllAsync()
        {
            return await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .ToListAsync();
        }

        public async Task<Pizza> GetByIdAsync(long id)
        {
            return await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .Where(p => p.Id == id)
                .FirstAsync();
        }

        public async Task<Pizza> AddAsync(AddPizzaViewModel model)
        {
            var newPizza = _dbContext.Pizzas.Add(new Pizza()
            {
                Name = model.Name,
                Description = model.Description
            });

            await _dbContext.SaveChangesAsync();
            return newPizza.Entity;
        }

        public async Task<Pizza> DeleteAsync(long id)
        {
            Pizza pizzaToRemove = await _dbContext.Pizzas
                .FindAsync(id);

            _dbContext.Pizzas.Remove(pizzaToRemove);

            await _dbContext.SaveChangesAsync();

            return pizzaToRemove;
        }

        public async Task<Pizza> AddIngredientAsync(long ingredientId, long pizzaId)
        {
            Pizza pizza = await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .Where(p => p.Id == pizzaId)
                .FirstAsync();

            Ingredient ingredient = await _dbContext.Ingredients
                .Include(i => i.Pizzas)
                .Where(i => i.Id == ingredientId)
                .FirstAsync();

            pizza.Ingredients.Add(ingredient);

            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task<Pizza> RemoveIngredientAsync(long ingredientId, long pizzaId)
        {
            Pizza pizza = await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .Where(p => p.Id == pizzaId)
                .FirstAsync();

            Ingredient ingredient = await _dbContext.Ingredients
                .Include(i => i.Pizzas)
                .Where(i => i.Id == ingredientId)
                .FirstAsync();

            pizza.Ingredients.Remove(ingredient);

            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task<Pizza> ChangeDescriptionAsync(long pizzaId, string newDescription)
        {
            Pizza pizza = await _dbContext.Pizzas
                .Where(p => p.Id == pizzaId)
                .FirstAsync();

            pizza.Description = newDescription;

            await _dbContext.SaveChangesAsync();
            return pizza;
        }
    }
}
