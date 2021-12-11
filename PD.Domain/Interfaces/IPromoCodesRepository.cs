using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Models;

namespace PD.Domain.Interfaces
{
    public interface IPromoCodesRepository
    {
        public Task<List<PromoCode>> GetAllAsync();
        public Task<PromoCode> GetByIdAsync(long id);
        public Task<PromoCode> AddAsync(AddPromoCodeViewModel model);
        public Task<PromoCode> DeleteAsync(long id);
    }
}
