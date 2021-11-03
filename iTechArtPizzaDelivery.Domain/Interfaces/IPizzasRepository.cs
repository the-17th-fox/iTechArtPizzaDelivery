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
        public Task<List<Pizza>> GetPizzasAsync();
        public Task<Pizza> GetPizzaAsync(int id);
        public Task<Pizza> CreatePizzaAsync(string name, string description);
        public Task<Pizza> RemovePizzaAsync(int id);
    }
}
