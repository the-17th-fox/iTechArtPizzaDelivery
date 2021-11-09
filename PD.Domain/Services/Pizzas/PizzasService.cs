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

        public async Task<List<Pizza>> GetPizzasAsync() => await _repository.GetPizzasAsync();

        public async Task<Pizza> GetPizzaAsync(int id) => await _repository.GetPizzaAsync(id);

        public async Task<Pizza> AddPizzaAsync(string name, string description) => await _repository.AddPizzaAsync(name, description);

        public async Task<Pizza> DeletePizzaAsync(int id) => await _repository.DeletePizzaAsync(id);

        public async Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId) => await _repository.AddIngredientToPizzaAsync(ingredientId, pizzaId);
    }
}
