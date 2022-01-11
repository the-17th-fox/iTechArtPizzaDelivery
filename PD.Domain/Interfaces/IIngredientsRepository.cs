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
        public Task AddAsync(Ingredient ingredient);
        public Task DeleteAsync(Ingredient ingredient);

        /// <summary>
        /// Searchs for the ingredient in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if the ingredient with the specified ID exists</returns>
        public Task<bool> ExistsAsync(long id);

        /// <summary>
        /// Searchs for the ingredient in the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns true if the ingredient with the specified name exists</returns>
        public Task<bool> ExistsAsync(string name);
    }
}
