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
        public Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId);
        public Task<Pizza> RemoveIngredientFromPizza(int ingredientId, int pizzaId);
    }
}
