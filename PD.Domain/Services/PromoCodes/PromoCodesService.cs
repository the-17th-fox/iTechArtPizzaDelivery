using AutoMapper;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
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
        private readonly IMapper _mapper;
        public PromoCodesService(IPromoCodesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PromoCodeViewModel> AddAsync(AddPromoCodeViewModel model)
        { 
            PromoCode promoCode = await _repository.AddAsync(model);
            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<PromoCodeViewModel> DeleteAsync(long id)
        {
            PromoCode promoCode = await _repository.DeleteAsync(id);
            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<List<ShortPromoCodeViewModel>> GetAllAsync()
        {
            List<PromoCode> promoCodes = await _repository.GetAllAsync();
            return _mapper.Map<List<ShortPromoCodeViewModel>>(promoCodes);
        }

        public async Task<PromoCodeViewModel> GetByIdAsync(long id)
        {
            PromoCode promoCode = await _repository.GetByIdAsync(id);
            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }
    }
}
