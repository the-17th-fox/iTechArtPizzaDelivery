using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PD.Domain.Constants.UserRoles;
using System.IdentityModel.Tokens.Jwt;

namespace PD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        
        public UsersController(UserManager<User> userManager, IUsersService usersService, IMapper mapper)
        {
            _userManager = userManager;
            _usersService = usersService;
            _mapper = mapper;
        }

        [ActionName(nameof(GetAllAsync))]
        [Route("all")]
        [HttpGet]
        public async Task<List<ShortUserViewModel>> GetAllAsync()
        {
            List<User> Users = await _usersService.GetAllAsync();
            return _mapper.Map<List<ShortUserViewModel>>(Users);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<UserViewModel> GetByIdAsync(long id)
        {
            User user = await _usersService.GetByIdAsync(id);
            return _mapper.Map<UserViewModel>(user);
        }

        [Route("register")]
        [HttpPost]
        public async Task<ShortUserViewModel> RegisterAsync(RegisterUserModel userModel)
        {
            User userToAdd = _mapper.Map<RegisterUserModel, User>(userModel);
            await _userManager.CreateAsync(userToAdd, userModel.Password);
            await _userManager.AddToRoleAsync(userToAdd, UserRoles.USER);
            await _usersService.AddAsync(userToAdd);
            return _mapper.Map<ShortUserViewModel>(userToAdd);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Email);

            if (user == null)
                return NotFound($"User with email '{userModel.Email}' was not found.");

            if (!await _userManager.CheckPasswordAsync(user, userModel.Password))
                return Unauthorized("Invalid email or password.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = _usersService.GetUserClaims(user, userRoles);
            var token = _usersService.GetNewToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                expiration = token.ValidTo,
                id = user.Id
            });
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<ShortUserViewModel> DeleteAsync(long id)
        {
            User userToRemove = await _usersService.DeleteAsync(id);
            return _mapper.Map<ShortUserViewModel>(userToRemove);
        }
    }
}
