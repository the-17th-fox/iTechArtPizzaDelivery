using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<PizzaViewModel> AddAsync(AddPizzaViewModel model)
        {
            Pizza pizza = await _repository.AddAsync(model);
            return _mapper.Map<PizzaViewModel>(pizza);
        }

        public async Task<PizzaIngredientsViewModel> AddIngredientAsync(long ingredientId, long pizzaId)
        {
            Pizza pizza = await _repository.AddIngredientAsync(ingredientId, pizzaId);
            return _mapper.Map<PizzaIngredientsViewModel>(pizza);
        }

        public async Task<PizzaViewModel> DeleteAsync(long id)
        {
            Pizza pizza = await _repository.DeleteAsync(id);
            return _mapper.Map<PizzaViewModel>(pizza);
        }

        public async Task<List<ShortPizzaViewModel>> GetAllAsync()
        {
            List<Pizza> pizzas = await _repository.GetAllAsync();
            return _mapper.Map<List<ShortPizzaViewModel>>(pizzas);
        }

        public async Task<PizzaViewModel> GetByIdAsync(long id)
        {
            Pizza pizza = await _repository.GetByIdAsync(id);
            return _mapper.Map<PizzaViewModel>(pizza);
        }

        public async Task<PizzaIngredientsViewModel> RemoveIngredientAsync(long ingredientId, long pizzaId)
        {
            Pizza pizza = await _repository.RemoveIngredientAsync(ingredientId, pizzaId);
            return _mapper.Map<PizzaIngredientsViewModel>(pizza);
        }

        public async Task<PizzaDescriptionViewModel> ChangeDescriptionAsync(long pizzaId, string newDescription)
        {
            Pizza pizza = await _repository.ChangeDescriptionAsync(pizzaId, newDescription);
            return _mapper.Map<PizzaDescriptionViewModel>(pizza);
        }
    }
}
