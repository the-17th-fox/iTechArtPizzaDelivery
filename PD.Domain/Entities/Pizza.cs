using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class Pizza
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
        public List<PizzaOrder> PizzaInOrders { get; set; }
        public List<IngredientPizza> IngredientsInPizza { get; set; }

    }
}
