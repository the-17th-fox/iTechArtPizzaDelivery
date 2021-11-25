using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IPizzasService : IBaseService<Pizza>
    {
        // Unique methods here
        public Task<Pizza> AddIngredientToPizzaAsync(long ingredientId, long pizzaId);
        public Task<Pizza> RemoveIngredientFromPizza(long ingredientId, long pizzaId);
    }
}
