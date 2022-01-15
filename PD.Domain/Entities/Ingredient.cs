using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class Ingredient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public List<IngredientPizza> IngredientInPizzas { get; set; }
    }
}
