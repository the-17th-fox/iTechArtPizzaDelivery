using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtIngredientDelivery.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService _ingredientsService;
        private readonly IMapper _mapper;
        public IngredientsController(IIngredientsService service, IMapper mapper)
        {
            _ingredientsService = service;
            _mapper = mapper;
        }


        [Route("[action]")]
        [ActionName(nameof(GetAllAsync))]
        [HttpGet]
        public async Task<List<ShortIngredientViewModel>> GetAllAsync()
        {
            List<Ingredient> ingredients = await _ingredientsService.GetAllAsync();
            return _mapper.Map<List<ShortIngredientViewModel>>(ingredients);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IngredientViewModel> GetAsync(long id)
        {
            Ingredient ingredient = await _ingredientsService.GetByIdAsync(id);
            return _mapper.Map<IngredientViewModel>(ingredient);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ShortIngredientViewModel> AddAsync(AddIngredientViewModel ingredientModel)
        {
            Ingredient newIngredient = _mapper.Map<AddIngredientViewModel, Ingredient>(ingredientModel);
            await _ingredientsService.AddAsync(newIngredient);
            return _mapper.Map<ShortIngredientViewModel>(newIngredient);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<ShortIngredientViewModel> DeleteAsync(long id)
        {
            Ingredient ingredientToRemove = await _ingredientsService.DeleteAsync(id);
            return _mapper.Map<ShortIngredientViewModel>(ingredientToRemove);
        }
    }
}
