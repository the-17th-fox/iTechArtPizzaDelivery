using Microsoft.AspNetCore.Mvc;
using PD.Domain.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PD.Domain.Constants.UsersRoles;

namespace PD.Web.Controllers.UsersControllers
{
    [Authorize(Roles = RolesNames.ADMIN)]
    [Route("api/users")]
    [ApiController]
    public class ElevatedRightsUsersController : Controller
    {
        
        private readonly IUsersService _usersService;
        
        public ElevatedRightsUsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _usersService.GetAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _usersService.GetByIdAsync(id));
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _usersService.DeleteAsync(id));
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> AddToRole(long userId, string role)
        {
            return Ok(await _usersService.AddToRole(userId, role));
        }
    }
}
