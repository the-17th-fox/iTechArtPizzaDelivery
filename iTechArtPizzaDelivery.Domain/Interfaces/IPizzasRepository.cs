using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArtPizzaDelivery.Domain.Entities;

namespace iTechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPizzasRepository
    {
        public List<Pizza> GetAllPizzas();
        public Pizza FindPizzaById(int id);
        public void PostPizza(string name, string description);
    }
}
