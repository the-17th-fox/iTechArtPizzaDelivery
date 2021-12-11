using PD.Domain.Entities;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IPizzasService
    {
        public Task<List<ShortPizzaViewModel>> GetAllAsync();
        public Task<PizzaViewModel> GetByIdAsync(long id);
        public Task<PizzaViewModel> AddAsync(AddPizzaViewModel model);
        public Task<PizzaViewModel> DeleteAsync(long id);
        public Task<PizzaIngredientsViewModel> AddIngredientAsync(long ingredientId, long pizzaId);
        public Task<PizzaIngredientsViewModel> RemoveIngredientAsync(long ingredientId, long pizzaId);
        public Task<PizzaDescriptionViewModel> ChangeDescriptionAsync(long pizzaId, string newDescription);
    }
}
