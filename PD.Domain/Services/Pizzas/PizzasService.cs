using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants.Exceptions;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;

namespace PD.Domain.Services
{
    public class PizzasService : IPizzasService
    {
        private readonly IPizzasRepository _pizzasRepository;
        private readonly IIngredientsRepository _ingredientsRepository;
        private readonly IMapper _mapper;
        public PizzasService(IPizzasRepository pizzasRepository,
            IIngredientsRepository ingredientsRepository, 
            IMapper mapper)
        {
            _pizzasRepository = pizzasRepository;
            _ingredientsRepository = ingredientsRepository;
            _mapper = mapper;
        }

        public async Task<PizzaViewModel> AddAsync(AddPizzaViewModel model)
        {
            // Checks if there is any pizza with the same name
            if (await _pizzasRepository.ExistsAsync(model.Name))
                throw new BadRequestException("There is already a pizza with this name.");

            var pizza = _mapper.Map<AddPizzaViewModel, Pizza>(model);

            await _pizzasRepository.AddAsync(pizza);

            return _mapper.Map<PizzaViewModel>(pizza);
        }

        public async Task<string> DeleteAsync(long id)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(id);
            // Checks if the pizza exists
            if (pizza == null)
                throw new BadRequestException("The pizza with the specified id does not exist.");

            await _pizzasRepository.DeleteAsync(pizza);

            return "The pizza has been deleted successfully.";
        }

        public async Task<PageViewModel<ShortPizzaViewModel>> GetAllAsync(PageSettingsViewModel pageSettings)
        {
            var pizzas = await _pizzasRepository.GetAllAsync();
            // Checks if there are any pizzas in the database
            if (pizzas.IsNullOrEmpty())
                throw new NotFoundException("No pizzas were found.");

            var pagedList = PagedList<Pizza>.ToPagedList(pizzas, pageSettings.PageNumber, pageSettings.PageSize);

            return _mapper.Map<PagedList<Pizza>, PageViewModel<ShortPizzaViewModel>>(pagedList);
        }

        public async Task<PizzaViewModel> GetByIdAsync(long id)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(id);
            // Checks if there is any pizza with the specified ID    
            if (pizza == null)
                throw new NotFoundException("The pizza was not found.");

            return _mapper.Map<PizzaViewModel>(pizza);
        }

        public async Task<PizzaIngredientsViewModel> AddIngredientAsync(long pizzaId, long ingredientId)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(pizzaId);
            // Checks if the pizza exists
            if (pizza == null)
                throw new BadRequestException("The pizza with the specified id does not exist.");

            var ingredient = await _ingredientsRepository.GetByIdAsync(ingredientId);
            // Checks if the pizza exists
            if (ingredient == null)
                throw new BadRequestException("The ingredient with the specified id does not exist.");

            // Сhecks that the specified pizza does not have the specified ingredient
            if (HasIngredientAsync(pizza, ingredient))
                throw new BadRequestException("Pizza has already has this ingredient.");

            await _pizzasRepository.AddIngredientAsync(pizza, ingredient);

            return _mapper.Map<PizzaIngredientsViewModel>(pizza);
        }

        public async Task<PizzaIngredientsViewModel> RemoveIngredientAsync(long pizzaId, long ingredientId)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(pizzaId);
            // Checks if the pizza exists
            if (pizza == null)
                throw new BadRequestException("The pizza with the specified id does not exist.");

            var ingredient = await _ingredientsRepository.GetByIdAsync(ingredientId);
            // Checks if the pizza exists
            if (ingredient == null)
                throw new BadRequestException("The ingredient with the specified id does not exist.");

            // Сhecks that the specified pizza has the specified ingredient
            if (!HasIngredientAsync(pizza, ingredient))
                throw new BadRequestException("Pizza already does not have this ingredient.");

            await _pizzasRepository.RemoveIngredientAsync(pizza, ingredient);

            return _mapper.Map<PizzaIngredientsViewModel>(pizza);
        }

        public async Task<PizzaDescriptionViewModel> ChangeDescriptionAsync(long pizzaId, string newDescription)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(pizzaId);
            // Checks if the pizza exists
            if (pizza == null)
                throw new BadRequestException("The pizza with the specified id does not exist.");

            await _pizzasRepository.ChangeDescriptionAsync(pizza, newDescription);

            return _mapper.Map<PizzaDescriptionViewModel>(pizza);
        }

        public bool HasIngredientAsync(Pizza pizza, Ingredient ingredient)
        {
            if (pizza.Ingredients.Contains(ingredient))
                return true;

            return false;
        }
    }
}
