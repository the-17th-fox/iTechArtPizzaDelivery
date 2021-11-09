using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PD.Domain.Interfaces
{
    public interface IPizzasRepository
    {
        public Task<List<Pizza>> GetPizzasAsync();
        public Task<Pizza> GetPizzaAsync(int id);
        public Task<Pizza> AddPizzaAsync(string name, string description);
        public Task<Pizza> DeletePizzaAsync(int id);
        public Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId);
    }
}
