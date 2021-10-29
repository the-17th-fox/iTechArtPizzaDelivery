using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTechArtPizzaDelivery.Domain.Entities;
using iTechArtPizzaDelivery.Domain.Interfaces;

namespace iTechArtPizzaDelivery.Domain.Services
{
    public class PizzasService : IPizzasService
    {
        private readonly IPizzasRepository _pizzasRepository;
        public PizzasService(IPizzasRepository context)
        {
            _pizzasRepository = context;
        }

        public async Task<List<Pizza>> GetPizzas() => await _pizzasRepository.GetPizzas();

        public async Task<Pizza> GetPizzaById(int id) => await _pizzasRepository.GetPizzaById(id);

        public async Task<Pizza> CreatePizza(Pizza pizza) => await _pizzasRepository.CreatePizza(pizza);
    }
}
