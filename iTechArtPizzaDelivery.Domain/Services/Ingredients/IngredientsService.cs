using PD.Domain.Entities;
using PD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IIngredientsRepository _repository;
        public IngredientsService(IIngredientsRepository repository) => _repository = repository;

        public async Task<Ingredient> AddIngredientAsync(string name)
        {
            if (name == null)
                return null;

            return await _repository.AddIngredientAsync(name);
        }

        public async Task<Ingredient> DeleteIngredientAsync(int id) => await _repository.DeleteIngredientAsync(id);

        public async Task<Ingredient> GetIngredientAsync(int id) => await _repository.GetIngredientAsync(id);

        public async Task<List<Ingredient>> GetIngredientsAsync() => await _repository.GetIngredientsAsync();
    }
}
