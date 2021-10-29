using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArtPizzaDelivery.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace iTechArtPizzaDelivery.Domain.Interfaces
{
    public interface IPizzasRepository
    {
        public Task<List<Pizza>> GetPizzas();
        public Task<Pizza> GetPizzaById(int id);
        public Task<Pizza> CreatePizza(Pizza pizza);
    }
}
