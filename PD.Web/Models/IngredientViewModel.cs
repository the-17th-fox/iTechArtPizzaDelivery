using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShortPizzaViewModel> Pizzas { get; set; } = new List<ShortPizzaViewModel>();
    }
}
