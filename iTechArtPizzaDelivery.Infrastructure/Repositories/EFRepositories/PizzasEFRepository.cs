using iTechArtPizzaDelivery.Domain.Entities;
using iTechArtPizzaDelivery.Domain.Interfaces;
using iTechArtPizzaDelivery.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class PizzasEFRepository : IPizzasRepository
    {
        private readonly PizzaDeliveryContext context;

        public PizzasEFRepository (PizzaDeliveryContext context) => this.context = context;

        public Pizza FindPizzaById(int id)
        {
            Pizza lookedPizza = (from pizza in context.Pizzas
                                 where (pizza.PizzaID == id)
                                 select pizza).FirstOrDefault();
            return lookedPizza;
        }

        public List<Pizza> GetAllPizzas() => context.Pizzas.ToList();

        public void PostPizza(string name, string description)
        {
            //context.Pizzas.Add(new Pizza
            //    (
            //        pizzaID: (ulong)context.Pizzas.Count(),
            //        name: name,
            //        description: description
            //    ));
            //TODO: Add post method
        }
    }
}
