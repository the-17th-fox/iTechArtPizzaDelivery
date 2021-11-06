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
        public PizzasController(IPizzasService pizzasService)
        {
            _pizzasService = pizzasService;
        }

        [ActionName(nameof(GetAllPizzasAsync))]
        [HttpGet]
        public async Task<List<Pizza>> GetAllPizzasAsync() => await _pizzasService.GetPizzasAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<Pizza> GetPizzaAsync(int id) => await _pizzasService.GetPizzaAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddPizzaAsync(string name, string description)
        {
            try
            {
                Pizza newPizza = await _pizzasService.AddPizzaAsync(name, description);

                return CreatedAtAction(nameof(GetAllPizzasAsync), 
                    new { id = newPizza.PizzaID }, newPizza);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                "Error while adding pizza");
            }
        }

        [HttpPut]
        public async Task<ActionResult> AddIngredientToPizza(int ingredientId, int pizzaId)
        {
            Pizza pizza = await _pizzasService.AddIngredientToPizzaAsync(ingredientId, pizzaId);
            try
            {
                

                return CreatedAtAction(nameof(GetAllPizzasAsync), 
                    new { id = pizza.PizzaID }, pizza);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                "Error while adding Ingredient to Pizza");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePizzaAsync(int id)
        {
            Pizza pizzaToRemove = await _pizzasService.DeletePizzaAsync(id);
            try
            {
                

                return CreatedAtAction(nameof(GetAllPizzasAsync), 
                    new { id = pizzaToRemove.PizzaID }, pizzaToRemove);
            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, 
                    "Error while removing pizza");
            }
        }
    }
}
