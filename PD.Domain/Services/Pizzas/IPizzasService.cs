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
        public Task<Pizza> AddIngredientToPizzaAsync(int ingredientId, int pizzaId);
        public Task<Pizza> RemoveIngredientFromPizza(int ingredientId, int pizzaId);
    }
}
