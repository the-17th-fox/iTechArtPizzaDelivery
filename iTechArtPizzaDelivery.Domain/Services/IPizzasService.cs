using iTechArtPizzaDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Services
{
    public interface IPizzasService
    {
        public Task<List<Pizza>> GetPizzas();
        public Task<Pizza> GetPizzaById(int id);
        public Task<Pizza> CreatePizza(Pizza pizza);
    }
}
