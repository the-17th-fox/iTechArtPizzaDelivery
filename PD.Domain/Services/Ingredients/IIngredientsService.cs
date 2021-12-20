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
        public Task<IActionResult> AddAsync(AddIngredientViewModel model);

        public Task<IActionResult> DeleteAsync(long id);

        public Task<IActionResult> GetAllAsync();

        public Task<IActionResult> GetByIdAsync(long id);
    }
}

