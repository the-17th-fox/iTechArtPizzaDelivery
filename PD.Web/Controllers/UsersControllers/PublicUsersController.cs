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

namespace PD.Web.Controllers.UsersControllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublicUsersController : Controller
    {

        private readonly IUsersService _usersService;

        public PublicUsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [AllowAnonymous]
        [Route("/account/register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterUserModel userModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult("Model is invalid.");

            return Ok(await _usersService.RegisterAsync(userModel));
        }

        [AllowAnonymous]
        [Route("/account/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserModel userModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult("Model is invalid.");

            return Ok(await _usersService.LoginAsync(userModel));
        }

        [Authorize(Roles = "User")]
        [Route("/account/delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(await _usersService.DeleteAsync(long.Parse(userId)));
        }
    }
}
