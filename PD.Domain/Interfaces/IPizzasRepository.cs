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
        public Task<Pizza> GetByNameAsync(string name);
        public Task<Pizza> AddAsync(Pizza pizza);
        public Task<Pizza> DeleteAsync(long id);
        public Task<Pizza> AddIngredientAsync(long pizzaId, long ingredientId);
        public Task<Pizza> RemoveIngredientAsync(long pizzaId, long ingredientId);
        public Task<Pizza> ChangeDescriptionAsync(long pizzaId, string newDescription);

        /// <summary>
        /// Searchs for the pizza with the specified ingredient ID in the database
        /// </summary>
        /// <param name="pizzaId"></param>
        /// <param name="ingredientId"></param>
        /// <returns>Returns true if the pizza with the provided ID has the ingredient with the provided ID</returns>
        public Task<bool> HasIngredientAsync(long pizzaId, long ingredientId);

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
