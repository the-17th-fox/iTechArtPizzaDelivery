using iTechArtPizzaDelivery.Domain.Entities;
using iTechArtPizzaDelivery.Domain.Interfaces;
using iTechArtPizzaDelivery.Infrastructure.Context;
using iTechArtPizzaDelivery.Infrastructure.Repositories.EFRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly IPizzasRepository _pizzasRepository;

        public PizzasController(IPizzasRepository repository)
        {
            _pizzasRepository = repository;
        }

        [Route("all")]
        [HttpGet]
        public async Task<List<Pizza>> GetAllPizzas()
        {
            return await _pizzasRepository.GetPizzas();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Pizza> GetPizzaById(int id)
        {
            return await _pizzasRepository.GetPizzaById(id);
        }

        [HttpPost/*("{name}&{description}")*/]
        public async Task<ActionResult> AddNewPizza(Pizza pizza)
        {
            throw new NotImplementedException();
        }
    }
}
