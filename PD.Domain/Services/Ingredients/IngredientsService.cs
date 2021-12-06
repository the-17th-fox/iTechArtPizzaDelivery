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

        public async Task<Ingredient> AddAsync(Ingredient entity) => await _repository.AddAsync(entity);

        public async Task<Ingredient> DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task<List<Ingredient>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Ingredient> GetByIdAsync(long id) => await _repository.GetByIdAsync(id);
    }
}
