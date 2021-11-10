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

        public async Task<Ingredient> AddAsync(string name) => await _repository.AddAsync(name);

        public async Task<Ingredient> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<Ingredient> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<List<Ingredient>> GetAllAsync() => await _repository.GetAllAsync();
    }
}
