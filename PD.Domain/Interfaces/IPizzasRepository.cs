using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PD.Domain.Interfaces
{
    public interface IPizzasRepository : IBaseRepository<Pizza>
    {
        // Unique methods here
        public Task<Pizza> AddIngredientToPizzaAsync(long ingredientId, long pizzaId);
        public Task<Pizza> RemoveIngredientFromPizza(long ingredientId, long pizzaId);
    }
}
