using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using PD.Domain.Constants.UsersRoles;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace PD.Web.Controllers
{
    [AllowAnonymous]
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

        [Authorize(Roles = "Administrator")]
        [ActionName(nameof(GetAllAsync))]
        [Route("all")]
        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var users = _userManager.Users;
            return Ok(_mapper.Map<List<ShortUserViewModel>>(users));
        }

        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            return Ok(_mapper.Map<UserViewModel>(user));
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterUserModel userModel)
        {
            //ISSUE: Method can't add user to DB
            User userToAdd = _mapper.Map<RegisterUserModel, User>(userModel);
            await _userManager.CreateAsync(userToAdd, userModel.Password);
            await _userManager.AddToRoleAsync(userToAdd, RolesNames.USER);
            return Ok(_mapper.Map<ShortUserViewModel>(userToAdd));
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel userModel)
        {
            var user = await _userManager.FindByNameAsync(userModel.Email.Normalize());

            if (user == null)
                return NotFound($"User with email '{userModel.Email}' was not found.");

            if (!await _userManager.CheckPasswordAsync(user, userModel.Password))
                return Unauthorized("Invalid email or password.");

            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = _usersService.GetUserClaims(user, userRoles);
            var token = _usersService.GetNewToken(authClaims);

            return Ok(new
            {
                roles = userRoles,
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                expiration = token.ValidTo,
                id = user.Id
            });
        }

        // Add two methods: 1 - for user without id request, 2 - for admin, who can specify id?
        [Authorize(Roles = "Administrator")]
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            User userToRemove = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(userToRemove);
            return Ok(_mapper.Map<ShortUserViewModel>(userToRemove));
        }
    }
}
