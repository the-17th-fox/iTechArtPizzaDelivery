using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace PD.Domain.Interfaces
{
    public interface IPromoCodesRepository
    {
        public Task<List<PromoCode>> GetAllAsync();
        public Task<PromoCode> GetByIdAsync(long id);
        public Task<PromoCode> GetByNameAsync(string name);
        public Task AddAsync(PromoCode promoCode);
        public Task DeleteAsync(PromoCode promoCode);

        /// <summary>
        /// Searchs for the promocode in the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns true if the promocode with the specified ID exists</returns>
        public Task<bool> ExistsAsync(long id);

        /// <summary>
        /// Searchs for the promocode in the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns true if the promocode with the specified name exists</returns>
        public Task<bool> ExistsAsync(string name);
    }
}
