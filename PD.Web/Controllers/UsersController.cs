using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private readonly IUsersService _usersService;
        public UsersController(IUsersService service) => _usersService = service;

        [ActionName(nameof(GetUsersAsync))]
        [HttpGet]
        public async Task<List<User>> GetUsersAsync() => await _usersService.GetAllAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<User> GetUserAsync(int id) => await _usersService.GetByIdAsync(id);
    }
}
