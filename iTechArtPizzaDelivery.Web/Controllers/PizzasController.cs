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
        private readonly IPizzasRepository _repository;

        public PizzasController(IPizzasRepository repository)
        {
            _repository = repository;
        }

        [Route("all")]
        [HttpGet]
        public List<Pizza> GetAllPizzas() => _repository.GetAllPizzas();

        [Route("{id}")]
        [HttpGet]
        public Pizza FindPizzaById(int id) => _repository.FindPizzaById(id);

        [HttpPost("{name}&{description}")]
        public void PostPizza(string name, string description) => _repository.PostPizza(name, description);
    }
}
