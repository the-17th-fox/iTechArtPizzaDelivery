using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;

namespace PD.Domain.Services
{
    public class PizzasService : IPizzasService
    {
        private readonly IPizzasRepository _repository;
        private readonly IMapper _mapper;
        public PizzasService(IPizzasRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> AddAsync(AddPizzaViewModel model)
        {
            // Checks if there is any pizza with the same name
            if (await _repository.ExistsAsync(model.Name))
                return new BadRequestObjectResult("There is already a pizza with this name");

            var pizza = _mapper.Map<AddPizzaViewModel, Pizza>(model);

            var result = await _repository.AddAsync(pizza);
            // Checks whether the adding was successful
            if (result == null)
                return new ObjectResult("An error occured while trying to add a new pizza");

            return new OkObjectResult(_mapper.Map<PizzaViewModel>(result));
        }

        public async Task<IActionResult> DeleteAsync(long id)
        {
            // Checks if the pizza exists
            if (!await _repository.ExistsAsync(id))
                return new BadRequestObjectResult("The pizza with the specified ID does not exist");

            var result = await _repository.DeleteAsync(id);
            // Сhecks whether the action was completed successfully
            if (result == null)
                return new ObjectResult("An error occured while trying to delete the pizza");

            return new OkObjectResult("The pizza has been deleted successfully");
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var pizzas = await _repository.GetAllAsync();
            // Checks if there are any pizzas in the database
            if (pizzas.IsNullOrEmpty())
                return new NotFoundObjectResult("No pizzas were found");

            return new OkObjectResult(_mapper.Map<List<ShortPizzaViewModel>>(pizzas));
        }

        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var pizza = await _repository.GetByIdAsync(id);
            // Checks if there is any pizza with the specified ID    
            if (pizza == null)
                return new NotFoundObjectResult("The pizza was not found");

            return new OkObjectResult(_mapper.Map<UserViewModel>(pizza));
        }

        public async Task<IActionResult> AddIngredientAsync(long pizzaId, long ingredientId)
        {
            // Checks if the pizza exists
            if (!await _repository.ExistsAsync(pizzaId))
                return new BadRequestObjectResult("The pizza with the specified ID does not exist");

            // Сhecks that the specified pizza does not have the specified ingredient
            if (await _repository.HasIngredientAsync(pizzaId, ingredientId))
                return new BadRequestObjectResult("Pizza already has this ingredient");

            var result = await _repository.AddIngredientAsync(pizzaId, ingredientId);
            // Checks whether the adding was successful
            if (result == null)
                return new ObjectResult("An error occured while trying to add a new ingredient to the pizza");

            return new OkObjectResult(_mapper.Map<PizzaIngredientsViewModel>(result));
        }

        public async Task<IActionResult> RemoveIngredientAsync(long pizzaId, long ingredientId)
        {
            // Checks if the pizza exists
            if (!await _repository.ExistsAsync(pizzaId))
                return new BadRequestObjectResult("The pizza with the specified ID does not exist");

            // Сhecks that the specified pizza does not have the specified ingredient
            if (!await _repository.HasIngredientAsync(pizzaId, ingredientId))
                return new BadRequestObjectResult("Pizza does not have this ingredient");

            var result = await _repository.RemoveIngredientAsync(pizzaId, ingredientId);
            // Checks whether the removing was successful
            if (result == null)
                return new ObjectResult("An error occured while trying to remove the ingredient from the pizza");

            return new OkObjectResult("The ingredient has been removed successfuly");
        }

        public async Task<IActionResult> ChangeDescriptionAsync(long pizzaId, string newDescription)
        {
            // Checks if the pizza exists
            if (!await _repository.ExistsAsync(pizzaId))
                return new BadRequestObjectResult("The pizza with the specified ID does not exist");

            var result = await _repository.ChangeDescriptionAsync(pizzaId, newDescription);
            if (result == null)
                return new ObjectResult("An error occured while trying to change the description of the pizza");

            return new OkObjectResult(_mapper.Map<PizzaDescriptionViewModel>(result));
        }
    }
}
