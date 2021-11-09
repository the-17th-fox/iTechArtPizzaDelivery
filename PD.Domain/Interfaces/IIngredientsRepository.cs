using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PD.Domain.Interfaces
{
    public interface IIngredientsRepository
    {
        public Task<List<Ingredient>> GetIngredientsAsync();
        public Task<Ingredient> GetIngredientAsync(int id);
        public Task<Ingredient> AddIngredientAsync(string name);
        public Task<Ingredient> DeleteIngredientAsync(int id);
    }
}
