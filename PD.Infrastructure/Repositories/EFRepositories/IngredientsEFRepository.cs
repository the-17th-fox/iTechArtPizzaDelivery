using Microsoft.EntityFrameworkCore;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
using PD.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class IngredientsEFRepository : IIngredientsRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public IngredientsEFRepository(PizzaDeliveryContext context) => _dbContext = context;

        public async Task<Ingredient> AddAsync(AddIngredientViewModel model)
        {
            var newIngredient = _dbContext.Ingredients.Add(new Ingredient()
            {
                Name = model.Name
            });

            await _dbContext.SaveChangesAsync();

            return newIngredient.Entity;
        }

        public async Task<Ingredient> DeleteAsync(long id)
        {
            Ingredient ingredientToRemove = await _dbContext.Ingredients.FindAsync(id);

            _dbContext.Ingredients.Remove(ingredientToRemove);
            await _dbContext.SaveChangesAsync();

            return ingredientToRemove;
        }

        public async Task<Ingredient> GetByIdAsync(long id) => await _dbContext.Ingredients.FindAsync(id);

        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await _dbContext.Ingredients
                .Include(i => i.Pizzas)
                .ToListAsync();
        }
    }
}
