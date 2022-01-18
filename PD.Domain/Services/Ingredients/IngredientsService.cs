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
            if (await _ingredientsRepository.ExistsAsync(model.Name))
                throw new BadRequestException("There is already an ingredient with this name.");

            var ingredient = _mapper.Map<AddIngredientViewModel, Ingredient>(model);

            await _ingredientsRepository.AddAsync(ingredient);

            return _mapper.Map<IngredientViewModel>(ingredient);
        }

        public async Task<string> DeleteAsync(long id)
        {
            var ingredient = await _ingredientsRepository.GetByIdAsync(id);
            // Checks if the pizza exists
            if (ingredient == null)
                throw new BadRequestException("The ingredient with the specified id does not exist.");

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
            var ingredient = await _ingredientsRepository.GetByIdAsync(id);
            // Checks if there is any ingredient with the specified ID    
            if (ingredient == null)
                throw new NotFoundException("The ingredient was not found.");

            return _mapper.Map<IngredientViewModel>(ingredient);
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var ingredient = await _ingredientsRepository.GetByIdAsync(id);
            return ingredient != null;
        }
    }
}
