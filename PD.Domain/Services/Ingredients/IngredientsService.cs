using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IIngredientsRepository _repository;
        private readonly IMapper _mapper;

        public IngredientsService(IIngredientsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> AddAsync(AddIngredientViewModel model)
        {
            // Checks if there is any ingredient with the same name
            if (await _repository.ExistsAsync(model.Name))
                return new BadRequestObjectResult("There is already an ingredient with this name");

            var ingredient = _mapper.Map<AddIngredientViewModel, Ingredient>(model);

            var result = await _repository.AddAsync(ingredient);
            // Checks whether the adding was successful
            if (result == null)
                return new ObjectResult("An error occured while trying to add a new ingredient");

            return new OkObjectResult(_mapper.Map<IngredientViewModel>(result));
        }

        public async Task<IActionResult> DeleteAsync(long id)
        {
            // Checks if the pizza exists
            if (await _repository.ExistsAsync(id))
                return new BadRequestObjectResult("The pizza with the specified ID does not exist");

            var result = await _repository.DeleteAsync(id);
            // Checks whether the adding was successful
            if (result == null)
                return new ObjectResult("An error occured while trying to delete the ingredient");

            return new OkObjectResult(_mapper.Map<IngredientViewModel>(result));
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var ingredients = await _repository.GetAllAsync();
            // Checks if there are any ingredients in the database
            if (ingredients.IsNullOrEmpty())
                return new NotFoundObjectResult("No ingredients were found");

            return new OkObjectResult(_mapper.Map<List<ShortIngredientViewModel>>(ingredients));
        }

        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var ingredient = await _repository.GetByIdAsync(id);
            // Checks if there is any ingredient with the specified ID    
            if (ingredient == null)
                return new NotFoundObjectResult("The ingredient was not found");

            return new OkObjectResult(_mapper.Map<UserViewModel>(ingredient));
        }
    }
}
