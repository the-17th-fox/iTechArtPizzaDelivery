using PD.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PD.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using PD.Domain.Constants.UsersRoles;

namespace PD.Web.Controllers.PizzasControllers
{
    [Authorize(Roles = RolesNames.ADMIN)]
    [Route("api/pizzas")]
    [ApiController]
    public class ElevatedRightsPizzasController : ControllerBase
    {
        private readonly IPizzasService _pizzasService;

        public ElevatedRightsPizzasController(IPizzasService service) => _pizzasService = service;

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddPizzaViewModel pizzaModel)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            return Ok(await _pizzasService.AddAsync(pizzaModel));
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _pizzasService.DeleteAsync(id));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddIngredientAsync(long pizzaId, long ingredientId)
        {
            return Ok(await _pizzasService.AddIngredientAsync(pizzaId, ingredientId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemoveIngredientAsync(long pizzaId, long ingredientId)
        {
            return Ok(await _pizzasService.RemoveIngredientAsync(pizzaId, ingredientId));
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> ChangeDescriptionAsync(long pizzaId, string newDescription)
        {
            return Ok(await _pizzasService.ChangeDescriptionAsync(pizzaId, newDescription));
        }
    }
}
