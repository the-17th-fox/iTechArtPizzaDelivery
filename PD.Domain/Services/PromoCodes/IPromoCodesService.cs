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
    public interface IPromoCodesService
    {
        public Task<List<ShortPromoCodeViewModel>> GetAllAsync();
        public Task<PromoCodeViewModel> GetByIdAsync(long id);
        public Task<PromoCodeViewModel> GetByNameAsync(string name);
        public Task<PromoCodeViewModel> AddAsync(AddPromoCodeViewModel promoCodeModel);
        public Task<string> DeleteAsync(long id);
    }
}
