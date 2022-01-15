﻿using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Domain.Models;
using PD.Domain.Constants.Exceptions;

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
                .Include(p => p.Orders)
                .Include(p => p.IngredientsInPizza)
                .Include(p => p.PizzaInOrders)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Pizza pizza)
        {
            try
            {
                _dbContext.Pizzas.Add(pizza);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new CreatingFailedException();
            }
        }

        public async Task DeleteAsync(Pizza pizza)
        {
            try
            {
                _dbContext.Pizzas.Remove(pizza);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DeletionFailedException();
            }
        }

        public async Task AddIngredientAsync(Pizza pizza, Ingredient ingredient)
        {   
            try
            {
                pizza.Ingredients.Add(ingredient);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task RemoveIngredientAsync(Pizza pizza, Ingredient ingredient)
        {
            try
            {
                pizza.Ingredients.Remove(ingredient);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task ChangeDescriptionAsync(Pizza pizza, string newDescription)
        {
            try
            {
                pizza.Description = newDescription;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UpdatingFailedException();
            }
        }

        public async Task<Pizza> GetByNameAsync(string name)
        {
            return await _dbContext.Pizzas
                .Include(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var pizza = await _dbContext.Pizzas
                .FindAsync(id);

            return pizza != null; 
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var pizza = await _dbContext.Pizzas
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();

            return pizza != null;
        }
    }
}
