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

        public async Task<PromoCode> AddAsync(string name, string description, float discountAmount)
            => await _repository.AddAsync(name, description, discountAmount);

        public async Task<PromoCode> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<PromoCode> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<List<PromoCode>> GetAllAsync() => await _repository.GetAllAsync();
    }
}
