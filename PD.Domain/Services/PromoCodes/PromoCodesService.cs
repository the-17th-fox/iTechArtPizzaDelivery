using PD.Domain.Entities;
using PD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public class PromoCodesService : IPromoCodesService
    {
        private readonly IPromoCodesRepository _repository;
        public PromoCodesService(IPromoCodesRepository repository) => _repository = repository;

        public async Task<PromoCode> AddPromoCodeAsync(string name, string description, float discountAmount)
            => await _repository.AddPromoCodeAsync(name, description, discountAmount);

        public async Task<PromoCode> DeletePromoCodeAsync(int id) => await _repository.DeletePromoCodeAsync(id);

        public async Task<PromoCode> GetPromoCodeAsync(int id) => await _repository.GetPromoCodeAsync(id);

        public async Task<List<PromoCode>> GetPromoCodesAsync() => await _repository.GetPromoCodesAsync();
    }
}
