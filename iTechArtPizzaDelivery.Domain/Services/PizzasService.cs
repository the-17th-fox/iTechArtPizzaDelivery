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

        public List<Pizza> GetAll()
        {
            return _pizzasRepository.GetAllPizzas();
        }

        public Pizza FindById(int id)
        {
            return _pizzasRepository.FindPizzaById(id);
        }

        public void Post(string name, string description)
        {
            _pizzasRepository.PostPizza(name, description);
        }
    }
}
