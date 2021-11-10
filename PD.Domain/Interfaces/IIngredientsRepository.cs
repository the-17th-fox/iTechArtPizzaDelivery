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
        public Task<List<Ingredient>> GetAllAsync();
        public Task<Ingredient> GetByIdAsync(int id);
        public Task<Ingredient> AddAsync(string name);
        public Task<Ingredient> DeleteAsync(int id);
    }
}
