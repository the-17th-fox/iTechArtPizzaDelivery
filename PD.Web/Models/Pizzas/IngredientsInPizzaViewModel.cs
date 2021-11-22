using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class IngredientsInPizzaViewModel
    {
        public int Id { get; set; }
        public List<ShortIngredientViewModel> Ingredients { get; set; } = new List<ShortIngredientViewModel>();
    }
}
