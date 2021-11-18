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
        [Route("[action]")]
        [HttpGet]
        public async Task<List<ShortPizzaViewModel>> GetAllAsync()
        {
            List<Pizza> pizzas = await _pizzasService.GetAllAsync();
            return _mapper.Map<List<ShortPizzaViewModel>>(pizzas);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<PizzaViewModel> GetByIdAsync(int id)
        {
            Pizza pizza = await _pizzasService.GetByIdAsync(id);
            return _mapper.Map<PizzaViewModel>(pizza);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ShortPizzaViewModel> AddAsync(AddPizzaViewModel pizzaModel)
        {
            Pizza pizzaToAdd = _mapper.Map<AddPizzaViewModel, Pizza>(pizzaModel);
            await _pizzasService.AddAsync(pizzaToAdd);
            return _mapper.Map<ShortPizzaViewModel>(pizzaToAdd);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<ShortPizzaViewModel> DeleteAsync(int id)
        {
            Pizza pizzaToRemove = await _pizzasService.DeleteAsync(id);
            return _mapper.Map<ShortPizzaViewModel>(pizzaToRemove);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IngredientsInPizzaViewModel> AddIngredientToPizza(int ingredientId, int pizzaId)
        {
            Pizza pizza = await _pizzasService.AddIngredientToPizzaAsync(ingredientId, pizzaId);
            return _mapper.Map<IngredientsInPizzaViewModel>(pizza);
        }

        [Route("[action]")]
        [HttpPut()]
        public async Task<IngredientsInPizzaViewModel> RemoveIngredientFromPizza(int ingredientId, int pizzaId)
        {
            Pizza pizza = await _pizzasService.RemoveIngredientFromPizza(ingredientId, pizzaId);
            return _mapper.Map<IngredientsInPizzaViewModel>(pizza);
        }
    }
}
