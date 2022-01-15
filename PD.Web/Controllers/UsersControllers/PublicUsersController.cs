using Microsoft.AspNetCore.Mvc;
using PD.Domain.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PD.Domain.Models;
using System.Security.Claims;
using PD.Domain.Constants.UsersRoles;

namespace PD.Web.Controllers.UsersControllers
{
    [Authorize]
    [Route("api/account")]
    [ApiController]
    public class PublicUsersController : Controller
    {
        private readonly IUsersService _usersService;

        public PublicUsersController(IUsersService usersService) => _usersService = usersService;

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterUserModel userModel)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            return Ok(await _usersService.RegisterAsync(userModel));
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel userModel)
        {
            if (!ModelState.IsValid)
                return ValidationProblem();

            return Ok(await _usersService.LoginAsync(userModel));
        }

        [Authorize(Roles = RolesNames.USER)]
        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _usersService.DeleteAsync(long.Parse(userId)));
        }

        [Authorize(Roles = RolesNames.USER)]
        [Route("im")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _usersService.GetByIdAsync(long.Parse(userId)));
        }
    }
}
