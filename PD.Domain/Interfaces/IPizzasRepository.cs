using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Models;

namespace PD.Domain.Interfaces
{
    public interface IPizzasRepository
    {
        public Task<List<Pizza>> GetAllAsync();
        public Task<Pizza> GetByIdAsync(long id);
        public Task<Pizza> AddAsync(AddPizzaViewModel model);
        public Task<Pizza> DeleteAsync(long id);
        public Task<Pizza> AddIngredientAsync(long ingredientId, long pizzaId);
        public Task<Pizza> RemoveIngredientAsync(long ingredientId, long pizzaId);
        public Task<Pizza> ChangeDescriptionAsync(long pizzaId, string newDescription);
    }
}
