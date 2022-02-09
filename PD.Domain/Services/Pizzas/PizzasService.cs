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
        private readonly IIngredientsService _ingredientsService;
        private readonly IFilesService _filesService;
        private readonly IMapper _mapper;
        public PizzasService(IPizzasRepository pizzasRepository,
            IIngredientsService ingredientsService,
            IFilesService filesService,
            IMapper mapper)
        {
            _pizzasRepository = pizzasRepository;
            _ingredientsService = ingredientsService;
            _filesService = filesService;
            _mapper = mapper;
        }

        public async Task<PizzaViewModel> AddAsync(AddPizzaViewModel model)
        {
            // Checks if there is any pizza with the same name
            await ExistsAsync(model.Name);

            var pizza = _mapper.Map<AddPizzaViewModel, Pizza>(model);

            var fileModel = _filesService.LoadFileAsync(model.ImageName);
            pizza.ImagePath = fileModel.FileStream.Name;

            await _pizzasRepository.AddAsync(pizza);

            return _mapper.Map<PizzaViewModel>(pizza);
        }

        public async Task<string> DeleteAsync(long id)
        {
            var pizza = await GetAndCheckByIdAsync(id);

            await _pizzasRepository.DeleteAsync(pizza);

            return "The pizza has been deleted successfully.";
        }

        public PageViewModel<ShortPizzaViewModel> GetAllAsync(PageSettingsViewModel pageSettings)
        {
            var pizzas = _pizzasRepository.GetAllAsync(pageSettings);

            return _mapper.Map<PagedList<Pizza>, PageViewModel<ShortPizzaViewModel>>(pizzas);
        }

        public async Task<PizzaViewModel> GetByIdAsync(long id)
        {
            var pizza = await GetAndCheckByIdWithoutTrackingAsync(id);

            return _mapper.Map<PizzaViewModel>(pizza);
        }

        public async Task<PizzaIngredientsViewModel> AddIngredientAsync(long pizzaId, long ingredientId)
        {
            var pizza = await GetAndCheckByIdAsync(pizzaId);

            var ingredient = await _ingredientsService.GetAndCheckByIdAsync(ingredientId);

            // Сhecks that the specified pizza does not have the specified ingredient
            if (HasIngredientAsync(pizza, ingredient))
                throw new BadRequestException("Pizza has already has this ingredient.");

            await _pizzasRepository.AddIngredientAsync(pizza, ingredient);

            return _mapper.Map<PizzaIngredientsViewModel>(pizza);
        }

        public async Task<PizzaIngredientsViewModel> RemoveIngredientAsync(long pizzaId, long ingredientId)
        {
            var pizza = await GetAndCheckByIdAsync(pizzaId);

            var ingredient = await _ingredientsService.GetAndCheckByIdAsync(ingredientId);

            if (!HasIngredientAsync(pizza, ingredient))
                throw new BadRequestException("Pizza does not have this ingredient.");

            await _pizzasRepository.RemoveIngredientAsync(pizza, ingredient);

            return _mapper.Map<PizzaIngredientsViewModel>(pizza);
        }

        public async Task<PizzaDescriptionViewModel> ChangeDescriptionAsync(long pizzaId, string newDescription)
        {
            var pizza = await GetAndCheckByIdAsync(pizzaId);

            await _pizzasRepository.ChangeDescriptionAsync(pizza, newDescription);

            return _mapper.Map<PizzaDescriptionViewModel>(pizza);
        }

        public bool HasIngredientAsync(Pizza pizza, Ingredient ingredient)
        {
            if (pizza.Ingredients.Contains(ingredient))
                return true;

            return false;
        }

        public async Task ExistsAsync(string name)
        {
            if (await _pizzasRepository.ExistsAsync(name))
                throw new BadRequestException("There is already a pizza with this name.");
        }

        public async Task<Pizza> GetAndCheckByIdAsync(long id)
        {
            var pizza = await _pizzasRepository.GetByIdAsync(id);
            if (pizza == null)
                throw new NotFoundException("The pizza with the specified id does not exist.");
            return pizza;
        }

        public async Task<Pizza> GetAndCheckByIdWithoutTrackingAsync(long id)
        {
            var pizza = await _pizzasRepository.GetByIdWithoutTrackingAsync(id);
            if (pizza == null)
                throw new NotFoundException("The pizza with the specified id does not exist.");
            return pizza;
        }
    }
}
