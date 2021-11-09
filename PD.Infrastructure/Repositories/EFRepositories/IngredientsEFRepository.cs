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
    public class IngredientsEFRepository : IIngredientsRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public IngredientsEFRepository(PizzaDeliveryContext context) => _dbContext = context;

        public async Task<Ingredient> AddIngredientAsync(string name)
        {
            var newIngredient = _dbContext.Ingredients.Add(new Ingredient
            {
                Name = name
            });

            await _dbContext.SaveChangesAsync();
            return newIngredient.Entity;
        }

        public async Task<Ingredient> DeleteIngredientAsync(int id)
        {
            Ingredient ingredientToRemove = await _dbContext.Ingredients.FindAsync(id);

            _dbContext.Ingredients.Remove(ingredientToRemove);
            await _dbContext.SaveChangesAsync();

            return ingredientToRemove;
        }

        public async Task<Ingredient> GetIngredientAsync(int id)
        {
            try
            {
                return await _dbContext.Ingredients
                            .FirstAsync(i => i.IngredientID == id);
            }
            catch (Exception)
            { 
                return null;
            }
        }

        public async Task<List<Ingredient>> GetIngredientsAsync() => await _dbContext.Ingredients.ToListAsync();
    }
}
