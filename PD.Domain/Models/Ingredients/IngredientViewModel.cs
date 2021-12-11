using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class IngredientViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ShortPizzaViewModel> Pizzas { get; set; }
    }
}
