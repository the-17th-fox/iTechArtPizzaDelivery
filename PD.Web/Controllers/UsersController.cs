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
            //ISSUE: Method can't add user to DB
        }

        //[AllowAnonymous]
        //[Route("login")]
        //[HttpPost]
        //public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel userModel)
        //{
            
        //}

        [Authorize(Roles = "Administrator")]
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _usersService.DeleteAsync(id));
        }

        [Authorize(Roles = "User")]
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<UserViewModel> DeleteAsync()
        {
            throw new NotImplementedException();
            //This methods stands for user self-deletion
        }
    }
}
