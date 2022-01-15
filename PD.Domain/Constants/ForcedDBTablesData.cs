using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Constants.ForcedDBTablesData
{
    public class ForcedDBTablesData
    {
        private static readonly List<Ingredient> _ingredients = new List<Ingredient>()
        {
            new Ingredient { Id = 1, Name = "Cheese" },
            new Ingredient { Id = 2, Name = "Tomato" },
            new Ingredient { Id = 3, Name = "Pizza Base" },
            new Ingredient { Id = 4, Name = "Olives" }
        };

        private static readonly List<Pizza> _pizzas = new List<Pizza>()
        {
            new Pizza { Id = 1, Name = "Cheese pizza", Description = "A lot of cheese", Price = 100 },
            new Pizza { Id = 2, Name = "Tomato pizza", Description = "A lot of tomatoes", Price = 200 },
            new Pizza { Id = 3, Name = "Pizza with olives", Description = "A lot of olives", Price = 300 }
        };

        public static List<Ingredient> GetIngredients() => _ingredients;

        public static List<Pizza> GetPizzas() => _pizzas;
    }
}
