using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            await _usersService.AddAsync(userToAdd);
            return _mapper.Map<ShortUserViewModel>(userToAdd);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel userModel)
        {
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user == null)
                return NotFound($"User with email '{userModel.Email}' was not found.");

            if (await _userManager.CheckPasswordAsync(user, userModel.Password))
            {
                return Ok(userModel);
                
            }
            return Unauthorized("Invalid password.");
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
