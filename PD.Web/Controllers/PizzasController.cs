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
using PD.Web.Models;

namespace PD.Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {		
        private readonly IPizzasService _pizzasService;
        private readonly IMapper _mapper;

        public PizzasController(IPizzasService service, IMapper mapper)
        {
            _pizzasService = service;
            _mapper = mapper;
        }

        [ActionName(nameof(GetAllAsync))]
        [HttpGet]
        public async Task<List<PizzaViewModel>> GetAllAsync()
        {
            return _mapper.Map<List<PizzaViewModel>>
                (await _pizzasService.GetAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<DetailPizzaViewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<DetailPizzaViewModel>
                (await _pizzasService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(string name, string description)
        {
            Pizza newPizza = await _pizzasService.AddAsync(name, description);

            return CreatedAtAction(nameof(GetAllAsync),
                new { id = newPizza.Id }, newPizza);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            Pizza pizzaToRemove = await _pizzasService.DeleteAsync(id);

            return CreatedAtAction(nameof(GetAllAsync),
                    new { id = pizzaToRemove.Id }, pizzaToRemove);
        }

        [Route("api/[controller]/[action]")]
        [HttpPut()]
        public async Task<ActionResult> AddIngredientToPizza(int ingredientId, int pizzaId)
        {
            Pizza pizza = await _pizzasService.AddIngredientToPizzaAsync(ingredientId, pizzaId);

            return CreatedAtAction(nameof(GetAllAsync),
                    new { id = pizza.Id }, pizza);
        }
    }
}
