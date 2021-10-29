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

        public async Task<List<Pizza>> GetPizzasAsync() => await _pizzasRepository.GetPizzasAsync();

        public async Task<Pizza> GetPizzaByIdAsync(int id) => await _pizzasRepository.GetPizzaByIdAsync(id);

        public async Task<Pizza> CreatePizzaAsync(Pizza pizza) => await _pizzasRepository.CreatePizzaAsync(pizza);
    }
}
