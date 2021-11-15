using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<Pizza> GetByIdAsync(int id)
        {
            return await _dbContext.Pizzas
                .Include(p => p.Ingredients)
                .Where(p => p.Id == id)
                .FirstAsync();
        }

        public async Task<Pizza> AddAsync(string name, string description)
        {
            var newPizza = _dbContext.Pizzas
                .Add(new Pizza
                {
                    Name = name,
                    Description = description,
                    Ingredients = new List<Ingredient>()
                });

            await _dbContext.SaveChangesAsync();
            return newPizza.Entity;
        }

        public async Task<Pizza> DeleteAsync(int id)
        {
            Pizza pizzaToRemove = await _dbContext.Pizzas
                .FindAsync(id);

            _dbContext.Pizzas.Remove(pizzaToRemove);

            await _dbContext.SaveChangesAsync();

            return pizzaToRemove;
        }

        public async Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId)
        {
            Pizza pizza = await _dbContext.Pizzas
                .FindAsync(pizzaId);

            Ingredient ingredient = await _dbContext.Ingredients
                .FindAsync(ingredientId);

            pizza.Ingredients.Add(ingredient);

            await _dbContext.SaveChangesAsync();
            return pizza;
        }

        public async Task<Pizza> RemoveIngredientFromPizza(int ingredientId, int pizzaId)
        {
            Pizza pizza = await _dbContext.Pizzas
                .FindAsync(pizzaId);

            Ingredient ingredient = pizza.Ingredients
                .Find(i => i.Id == ingredientId);

            pizza.Ingredients.Remove(ingredient);

            await _dbContext.SaveChangesAsync();
            return pizza;
        }
    }
}
