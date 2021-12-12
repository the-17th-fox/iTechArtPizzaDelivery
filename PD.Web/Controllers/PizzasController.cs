using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Services;
using PD.Infrastructure.Context;
using PD.Infrastructure.Repositories.EFRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PD.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace PD.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {		
        private readonly IPizzasService _pizzasService;

        public PizzasController(IPizzasService service)
        {
            _pizzasService = service;
        }

        [Authorize(Roles = "User")]
        [ActionName(nameof(GetAllAsync))]
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _pizzasService.GetAllAsync());
        }

        [Authorize(Roles = "User")]
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _pizzasService.GetByIdAsync(id));
        }

        [Authorize(Roles = "Administrator")]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddPizzaViewModel pizzaModel)
        {
            return Ok(await _pizzasService.AddAsync(pizzaModel));
        }

        [Authorize(Roles = "Administrator")]
        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _pizzasService.DeleteAsync(id));
        }

        [Authorize(Roles = "Administrator")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> AddIngredientAsync(long ingredientId, long pizzaId)
        {
            return Ok(await _pizzasService.AddIngredientAsync(ingredientId, pizzaId));
        }

        [Authorize(Roles = "Administrator")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> RemoveIngredientAsync(long ingredientId, long pizzaId)
        {
            return Ok(await _pizzasService.RemoveIngredientAsync(ingredientId, pizzaId));
        }

        [Authorize(Roles = "Administrator")]
        [Route("[action]")]
        [HttpPut()]
        public async Task<IActionResult> ChangeDescriptionAsync(long pizzaId, string newDescription)
        {
            return Ok(await _pizzasService.ChangeDescriptionAsync(pizzaId, newDescription));
        }
    }
}
