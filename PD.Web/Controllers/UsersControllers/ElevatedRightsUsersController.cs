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
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatedRightsUsersController : Controller
    {
        
        private readonly IUsersService _usersService;
        
        public ElevatedRightsUsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [ActionName(nameof(GetAllAsync))]
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

        [Route("delete/{id}")]
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
