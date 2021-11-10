using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtIngredientDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService _ingredientsService;
        public IngredientsController(IIngredientsService service) => _ingredientsService = service;

        [ActionName(nameof(GetAllIngredientsAsync))]
        [HttpGet]
        public async Task<List<Ingredient>> GetAllIngredientsAsync() => await _ingredientsService.GetAllAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<Ingredient> GetIngredientAsync(int id) => await _ingredientsService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddIngredientAsync(string name)
        {
            Ingredient newIngredient = await _ingredientsService.AddAsync(name);

            return CreatedAtAction(nameof(GetAllIngredientsAsync), new { id = newIngredient.IngredientID }, newIngredient);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteIngredientAsync(int id)
        {
            Ingredient IngredientToRemove = await _ingredientsService.DeleteAsync(id);

            return CreatedAtAction(nameof(GetAllIngredientsAsync), new { id = IngredientToRemove.IngredientID }, IngredientToRemove);
        }
    }
}
