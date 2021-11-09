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
        public Task<List<PromoCode>> GetPromoCodesAsync();
        public Task<PromoCode> GetPromoCodeAsync(int id);
        public Task<PromoCode> AddPromoCodeAsync(string name, string description, float discountAmount);
        public Task<PromoCode> DeletePromoCodeAsync(int id);
    }
}
