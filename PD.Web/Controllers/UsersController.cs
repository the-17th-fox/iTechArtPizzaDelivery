using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using PD.Domain.Constants.UsersRoles;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using PD.Domain.Models;
using System;
using System.Security.Claims;

namespace PD.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        
        private readonly IUsersService _usersService;
        
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [Authorize(Roles = "Administrator")]
        [ActionName(nameof(GetAllAsync))]
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _usersService.GetAllAsync());
        }

        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _usersService.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterUserModel userModel)
        {
            return Ok(await _usersService.RegisterAsync(userModel));
            
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel userModel)
        {
            return Ok(await _usersService.LoginAsync(userModel));
        }

        [Authorize(Roles = "Administrator")]
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _usersService.DeleteAsync(id));
        }

        [Authorize(Roles = "User")]
        [Route("delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _usersService.DeleteAsync(long.Parse(userId)));
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> AddToRole(long userId, string role)
        {
            return Ok(await _usersService.AddToRole(userId, role));
        }
    }
}
