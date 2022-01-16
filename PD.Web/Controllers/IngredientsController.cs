using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Constants.UsersRoles;
using PD.Domain.Models;
using PD.Domain.Services;
using System.Threading.Tasks;

namespace iTechArtIngredientDelivery.Web.Controllers
{
    [Authorize(Roles = RolesNames.ADMIN)]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService _ingredientsService;
        public IngredientsController(IIngredientsService service) => _ingredientsService = service;

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _ingredientsService.GetAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _ingredientsService.GetByIdAsync(id));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddIngredientViewModel model)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            return Ok(await _ingredientsService.AddAsync(model));
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _ingredientsService.DeleteAsync(id));
        }
    }
}
