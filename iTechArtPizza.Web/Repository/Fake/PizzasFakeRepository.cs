using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArtPizza.Web.Entities;
using iTechArtPizza.Web.Interfaces;

namespace iTechArtPizza.Web.Repository.Fake
{
    public class PizzasFakeRepository : IPizzasRepository
    {
        public List<Pizza> GetAll()
        {
            return new List<Pizza>
            {
                new Pizza
                (
                    title: "Banana Pizza",
                    description: "This is a Banana pizza"
                ),

                new Pizza
                (
                    title: "Tomato Pizza",
                    description: "This is a Tomato pizza"
                ),
            };
        }
    }
}
