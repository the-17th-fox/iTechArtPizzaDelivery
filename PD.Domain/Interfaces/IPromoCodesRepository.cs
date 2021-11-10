using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PD.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PD.Domain.Interfaces
{
    public interface IPromoCodesRepository
    {
        public Task<List<PromoCode>> GetAllAsync();
        public Task<PromoCode> GetByIdAsync(int id);
        public Task<PromoCode> AddAsync(string name, string description, float discountAmount);
        public Task<PromoCode> DeleteAsync(int id);
    }
}
