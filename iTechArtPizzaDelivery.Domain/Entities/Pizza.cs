using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class Pizza
    {
        public Pizza(int pizzaID, string name, string description)
        {
            PizzaID = pizzaID;
            Name = name;
            Description = description;
        }

        public int PizzaID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public /*Image*/ Image { get; set; }

    }
}
