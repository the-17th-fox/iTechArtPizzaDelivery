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

        public Pizza FindPizzaById(ulong id)
        {
            throw new NotImplementedException();
        }

        public List<Pizza> GetAllPizzas() => context.Pizzas.ToList();

        public void PostPizza(string name, string description)
        {
            throw new NotImplementedException();
        }
    }
}
