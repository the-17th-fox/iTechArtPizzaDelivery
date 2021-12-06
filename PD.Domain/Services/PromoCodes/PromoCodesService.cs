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

        public async Task<PromoCode> AddAsync(PromoCode entity) => await _repository.AddAsync(entity);

        public async Task<PromoCode> DeleteAsync(long id) => await _repository.DeleteAsync(id);

        public async Task<List<PromoCode>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<PromoCode> GetByIdAsync(long id) => await _repository.GetByIdAsync(id);
    }
}
