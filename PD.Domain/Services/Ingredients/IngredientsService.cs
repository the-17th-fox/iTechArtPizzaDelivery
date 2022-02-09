using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants.Exceptions;
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
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IMapper _mapper;

        public IngredientsService(IIngredientsRepository repository, IMapper mapper)
        {
            _ingredientsRepository = repository;
            _mapper = mapper;
        }

        public async Task<IngredientViewModel> AddAsync(AddIngredientViewModel model)
        {
            // Checks if there is any ingredient with the same name
            await ExistsAsync(model.Name);

            var ingredient = _mapper.Map<AddIngredientViewModel, Ingredient>(model);

            await _ingredientsRepository.AddAsync(ingredient);

            return _mapper.Map<IngredientViewModel>(ingredient);
        }

        public async Task<string> DeleteAsync(long id)
        {
            var ingredient = await GetAndCheckByIdAsync(id);

            await _ingredientsRepository.DeleteAsync(ingredient);

            return "The ingredient has been deleted successfully.";
        }

        public async Task<List<ShortIngredientViewModel>> GetAllAsync()
        {
            var ingredients = await _ingredientsRepository.GetAllAsync();

            return _mapper.Map<List<ShortIngredientViewModel>>(ingredients);
        }

        public async Task<IngredientViewModel> GetByIdAsync(long id)
        {
            var ingredient = await GetAndCheckByIdWithoutTrackingAsync(id);

            return _mapper.Map<IngredientViewModel>(ingredient);
        }

        public async Task ExistsAsync(string name)
        {
            if(await _ingredientsRepository.ExistsAsync(name))
                throw new BadRequestException("There is already an ingredient with this name.");
        }

        public async Task<Ingredient> GetAndCheckByIdAsync(long id)
        {
            var ingredient = await _ingredientsRepository.GetByIdAsync(id);
            if (ingredient == null)
                throw new NotFoundException("The ingredient with the specified id does not exist.");
            return ingredient;
        }

        public async Task<Ingredient> GetAndCheckByIdWithoutTrackingAsync(long id)
        {
            var ingredient = await _ingredientsRepository.GetByIdWithoutTrackingAsync(id);
            if (ingredient == null)
                throw new NotFoundException("The ingredient with the specified id does not exist.");
            return ingredient;
        }
    }
}
