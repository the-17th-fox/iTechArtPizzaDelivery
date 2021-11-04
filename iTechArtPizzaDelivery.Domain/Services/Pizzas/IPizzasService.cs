using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IPizzasService
    {
        public Task<List<Pizza>> GetPizzasAsync();
        public Task<Pizza> GetPizzaAsync(int id);
        public Task<Pizza> AddPizzaAsync(string name, string description);
        public Task<Pizza> DeletePizzaAsync(int id);
        public Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId);
    }
}
