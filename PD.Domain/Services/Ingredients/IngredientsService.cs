using AutoMapper;
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

        public async Task<IngredientViewModel> AddAsync(AddIngredientViewModel model)
        {
            Ingredient ingredient = await _repository.AddAsync(model);
            return _mapper.Map<IngredientViewModel>(ingredient);
        }

        public async Task<IngredientViewModel> DeleteAsync(long id)
        {
            Ingredient ingredient = await _repository.DeleteAsync(id);
            return _mapper.Map<IngredientViewModel>(ingredient);
        }

        public async Task<List<ShortIngredientViewModel>> GetAllAsync()
        {
            List<Ingredient> ingredients = await _repository.GetAllAsync();
            return _mapper.Map<List<ShortIngredientViewModel>>(ingredients);
        }

        public async Task<IngredientViewModel> GetByIdAsync(long id)
        {
            Ingredient ingredient = await _repository.GetByIdAsync(id);
            return _mapper.Map<IngredientViewModel>(ingredient);
        }
    }
}
