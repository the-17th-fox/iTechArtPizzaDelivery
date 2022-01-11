using Microsoft.EntityFrameworkCore;
using PD.Domain.Constants.Exceptions;
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

        public async Task AddAsync(Ingredient ingredient)
        {
            try
            {
                _dbContext.Ingredients.Add(ingredient);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new CreatingFailedException();
            }
        }

        public async Task DeleteAsync(Ingredient ingredient)
        {
            try
            {
                _dbContext.Ingredients.Remove(ingredient);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DeletionFailedException();
            }
        }

        public async Task<Ingredient> GetByIdAsync(long id) => await _dbContext.Ingredients.FindAsync(id);

        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await _dbContext.Ingredients
                .Include(i => i.Pizzas)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var ingredient = await _dbContext.Ingredients
                .FindAsync(id);

            return ingredient != null;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var ingredient = await _dbContext.Ingredients
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();

            return ingredient != null;
        }
    }
}
