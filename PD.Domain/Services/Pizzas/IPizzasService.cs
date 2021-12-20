using Microsoft.AspNetCore.Mvc;
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
        public Task<IActionResult> GetAllAsync();
        public Task<IActionResult> GetByIdAsync(long id);
        public Task<IActionResult> AddAsync(AddPizzaViewModel model);
        public Task<IActionResult> DeleteAsync(long id);
        public Task<IActionResult> AddIngredientAsync(long pizzaId, long ingredientId);
        public Task<IActionResult> RemoveIngredientAsync(long pizzaId, long ingredientId);
        public Task<IActionResult> ChangeDescriptionAsync(long pizzaId, string newDescription);
    }
}
