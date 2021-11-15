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
        public Task<List<Pizza>> GetAllAsync();
        public Task<Pizza> GetByIdAsync(int id);
        public Task<Pizza> AddAsync(string name, string description);
        public Task<Pizza> DeleteAsync(int id);
        public Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId);
        public Task<Pizza> RemoveIngredientFromPizza(int ingredientId, int pizzaId);
    }
}
