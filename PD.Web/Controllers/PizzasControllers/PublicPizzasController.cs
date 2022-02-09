using PD.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PD.Domain.Constants.UsersRoles;
using PD.Domain.Models;

namespace PD.Web.Controllers.PizzasControllers
{
    [Authorize(Roles = RolesNames.USER)]
    [Route("api/pizzas")]
    [ApiController]
    public class PublicPizzasController : ControllerBase
    {		
        private readonly IPizzasService _pizzasService;

        public PublicPizzasController(IPizzasService service) => _pizzasService = service;

        [HttpGet]
        public IActionResult GetAllAsync([FromQuery] PageSettingsViewModel pageSettings)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            return Ok(_pizzasService.GetAllAsync(pageSettings));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _pizzasService.GetByIdAsync(id));
        }
    }
}
