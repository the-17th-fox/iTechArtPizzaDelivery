using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IPizzasService
    {
        public PageViewModel<ShortPizzaViewModel> GetAllAsync(PageSettingsViewModel pageSettings);
        public Task<PizzaViewModel> GetByIdAsync(long id);
        public Task<PizzaViewModel> AddAsync(AddPizzaViewModel model);
        public Task<string> DeleteAsync(long id);
        public Task<PizzaIngredientsViewModel> AddIngredientAsync(long pizzaId, long ingredientId);
        public Task<PizzaIngredientsViewModel> RemoveIngredientAsync(long pizzaId, long ingredientId);
        public Task<PizzaDescriptionViewModel> ChangeDescriptionAsync(long pizzaId, string newDescription);
        public bool HasIngredientAsync(Pizza pizza, Ingredient ingredient);
        public Task ExistsAsync(string name);
        public Task<Pizza> GetAndCheckByIdAsync(long id);
        public Task<Pizza> GetAndCheckByIdWithoutTrackingAsync(long id);
    }
}
