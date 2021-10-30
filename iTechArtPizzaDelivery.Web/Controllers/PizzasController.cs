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

        [ActionName(nameof(GetAllPizzasAsync))]
        [Route("all")]
        [HttpGet]
        public async Task<List<Pizza>> GetAllPizzasAsync()
        {
            return await _pizzasRepository.GetPizzasAsync();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Pizza> GetPizzaByIdAsync(int id)
        {
            return await _pizzasRepository.GetPizzaByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePizzaAsync(Pizza pizza)
        {
            try
            {
                if (pizza == null)
                    return BadRequest();

                Pizza newPizza = await _pizzasRepository.CreatePizzaAsync(pizza);

                return CreatedAtAction(nameof(GetAllPizzasAsync), new { id = pizza.PizzaID }, newPizza);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error while adding pizza");
            }
        }
    }
}
