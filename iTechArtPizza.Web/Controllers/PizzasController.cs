using iTechArtPizza.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArtPizza.Web.Repository.Fake;

namespace iTechArtPizza.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzasFakeRepository _pizzasFakeRepository = new PizzasFakeRepository();

        [HttpGet]
        public List<Pizza> GetPizzasInfo() => _pizzasFakeRepository.GetPizzasInfo();

        [HttpPost("{title}${description}")]
        public void PostPizza(string name, string description) => _pizzasFakeRepository.PostPizza(name, description);

        //[HttpGet]
        //public List<Pizza> GetPizzasTitles() => _pizzasFakeRepository.GetPizzasTitles();


    }
}
