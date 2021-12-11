using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class PizzaIngredientsViewModel
    {
        public long Id { get; set; }
        public List<ShortIngredientViewModel> Ingredients { get; set; }
    }
}
