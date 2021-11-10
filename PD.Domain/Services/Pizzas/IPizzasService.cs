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
        public Task<List<Pizza>> GetAllAsync();
        public Task<Pizza> GetByIdAsync(int id);
        public Task<Pizza> AddAsync(string name, string description);
        public Task<Pizza> DeleteAsync(int id);
        public Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId);
    }
}
