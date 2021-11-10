using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IPromoCodesService
    {
        public Task<List<PromoCode>> GetAllAsync();
        public Task<PromoCode> GetByIdAsync(int id);
        public Task<PromoCode> AddAsync(string name, string description, float discountAmount);
        public Task<PromoCode> DeleteAsync(int id);
    }
}
