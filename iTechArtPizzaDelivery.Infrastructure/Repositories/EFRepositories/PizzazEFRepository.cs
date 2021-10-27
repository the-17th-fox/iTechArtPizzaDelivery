using iTechArtPizzaDelivery.Domain.Entities;
using iTechArtPizzaDelivery.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories
{
    public class PizzazEFRepository
    {
        private readonly PizzaDeliveryContext context;

        public PizzazEFRepository (PizzaDeliveryContext context) => this.context = context;

        public List<Pizza> GetPizzasInfo() => context.Pizzas.ToList();
    }
}
