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
    public interface IIngredientsService
    {
        public Task<IngredientViewModel> AddAsync(AddIngredientViewModel model);

        public Task<string> DeleteAsync(long id);

        public Task<List<ShortIngredientViewModel>> GetAllAsync();

        public Task<IngredientViewModel> GetByIdAsync(long id);
        public Task<bool> ExistsAsync(long id);
    }
}

