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
        public Task<List<PromoCode>> GetPromoCodesAsync();
        public Task<PromoCode> GetPromoCodeAsync(int id);
        public Task<PromoCode> AddPromoCodeAsync(string name, string description, float discountAmount);
        public Task<PromoCode> DeletePromoCodeAsync(int id);
    }
}
