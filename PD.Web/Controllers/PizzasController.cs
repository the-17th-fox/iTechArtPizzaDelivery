using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Services;
using PD.Infrastructure.Context;
using PD.Infrastructure.Repositories.EFRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {		
        private readonly IPizzasService _pizzasService;
        public PizzasController(IPizzasService service)
        {
            _pizzasService = service;
        }

        [ActionName(nameof(GetAllPizzasAsync))]
        [HttpGet]
        public async Task<List<Pizza>> GetAllPizzasAsync() => await _pizzasService.GetAllAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<Pizza> GetPizzaAsync(int id) => await _pizzasService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddPizzaAsync(string name, string description)
        {
            Pizza newPizza = await _pizzasService.AddAsync(name, description);

            return CreatedAtAction(nameof(GetAllPizzasAsync),
                new { id = newPizza.PizzaID }, newPizza);
        }

        [HttpPut]
        public async Task<ActionResult> AddIngredientToPizza(int ingredientId, int pizzaId)
        {
            Pizza pizza = await _pizzasService.AddIngredientToPizzaAsync(ingredientId, pizzaId);

            return CreatedAtAction(nameof(GetAllPizzasAsync),
                    new { id = pizza.PizzaID }, pizza);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePizzaAsync(int id)
        {
            Pizza pizzaToRemove = await _pizzasService.DeleteAsync(id);

            return CreatedAtAction(nameof(GetAllPizzasAsync),
                    new { id = pizzaToRemove.PizzaID }, pizzaToRemove);
        }
    }
}
