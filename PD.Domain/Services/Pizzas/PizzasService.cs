using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Domain.Entities;
using PD.Domain.Interfaces;

namespace PD.Domain.Services
{
    public class PizzasService : IPizzasService
    {
        private readonly IPizzasRepository _repository;
        public PizzasService(IPizzasRepository repository) => _repository = repository;

        public async Task<Pizza> AddAsync(Pizza entity) => await _repository.AddAsync(entity);

        public async Task<Pizza> AddIngredientToPizzaAsync(long ingredientId, long pizzaId) 
            => await _repository.AddIngredientToPizzaAsync(ingredientId, pizzaId);

        public async Task<Pizza> DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task<List<Pizza>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Pizza> GetByIdAsync(long id) => await _repository.GetByIdAsync(id);

        public async Task<Pizza> RemoveIngredientFromPizza(long ingredientId, long pizzaId)
            => await _repository.RemoveIngredientFromPizza(ingredientId, pizzaId);
    }
}
