using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IIngredientsService
    {
        public Task<List<Ingredient>> GetIngredientsAsync();
        public Task<Ingredient> GetIngredientAsync(int id);
        public Task<Ingredient> AddIngredientAsync(string name);
        public Task<Ingredient> DeleteIngredientAsync(int id);
    }
}
