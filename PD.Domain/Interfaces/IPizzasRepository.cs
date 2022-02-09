using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;

namespace PD.Domain.Interfaces
{
    public interface IPizzasRepository
    {
        public PagedList<Pizza> GetAllAsync(PageSettingsViewModel pageSettings);
        public Task<Pizza> GetByIdWithoutTrackingAsync(long id);
        public Task<Pizza> GetByIdAsync(long id);
        public Task AddAsync(Pizza pizza);
        public Task DeleteAsync(Pizza pizza);
        public Task AddIngredientAsync(Pizza pizza, Ingredient ingredient);
        public Task RemoveIngredientAsync(Pizza pizza, Ingredient ingredient);
        public Task ChangeDescriptionAsync(Pizza pizza, string newDescription);

        /// <summary>
        /// Searchs for the pizza in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if the pizza with the specified ID exists</returns>
        public Task<bool> ExistsAsync(long id);

        /// <summary>
        /// Searchs for the pizza in the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns true if the pizza with the specified name exists</returns>
        public Task<bool> ExistsAsync(string name);
    }
}
