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

        private List<Pizza> CreatePizzasList()
        {
            ulong newId = 0;

            return new List<Pizza>
            {
                new Pizza
                (
                    id: newId++,
                    title: "Banana Pizza",
                    description: "This is a Banana pizza"
                ),

                    new Pizza
                (
                    id: newId++,
                    title: "Tomato Pizza",
                    description: "This is a Tomato pizza"
                ),

                    new Pizza
                (
                    id: newId++,
                    title: "Cheese Pizza",
                    description: "This is a Cheese pizza"
                ),

                    new Pizza
                (
                    id: newId++,
                    title: "Apple Pizza",
                    description: "This is an Apple pizza"
                ),
            };
        }

        public List<Pizza> GetAll()
        {
            return CreatePizzasList();
        }
    }
}
