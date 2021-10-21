using iTechArtPizzaDelivery.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArtPizzaDelivery.Web.Repository.Fake;

namespace iTechArtPizzaDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzasFakeRepository _pizzasFakeRepository = new PizzasFakeRepository();

        [Route("all")]
        [HttpGet]
        public List<Pizza> GetPizzasInfo() => _pizzasFakeRepository.GetPizzasInfo();

        [Route("{id}")]
        [HttpGet]
        public Pizza FindPizzaById(ulong id) => _pizzasFakeRepository.FindPizzaById(id);

        [HttpPost("{name}&{description}")]
        public void PostPizza(string name, string description) => _pizzasFakeRepository.PostPizza(name, description);
    }
}
