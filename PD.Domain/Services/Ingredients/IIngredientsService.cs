using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IIngredientsService
    {
        public Task<List<Ingredient>> GetAllAsync();
        public Task<Ingredient> GetByIdAsync(int id);
        public Task<Ingredient> AddAsync(string name);
        public Task<Ingredient> DeleteAsync(int id);
    }
}
