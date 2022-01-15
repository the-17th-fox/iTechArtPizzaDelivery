using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class IngredientPizza
    {
        public long PizzaId { get; set; }
        public long IngredientId { get; set; }

        public Pizza Pizza { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
