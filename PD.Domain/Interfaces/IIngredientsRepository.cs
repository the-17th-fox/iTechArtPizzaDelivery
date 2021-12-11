using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Models;

namespace PD.Domain.Interfaces
{
    public interface IIngredientsRepository
    {
        public Task<List<Ingredient>> GetAllAsync();
        public Task<Ingredient> GetByIdAsync(long id);
        public Task<Ingredient> AddAsync(AddIngredientViewModel model);
        public Task<Ingredient> DeleteAsync(long id);
    }
}
