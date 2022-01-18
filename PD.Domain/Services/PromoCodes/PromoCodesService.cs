using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants.Exceptions;
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

        public async Task<List<ShortPromoCodeViewModel>> GetAllAsync()
        {
            var promoCodes = await _repository.GetAllAsync();

            return _mapper.Map<List<ShortPromoCodeViewModel>>(promoCodes);
        }

        public async Task<PromoCodeViewModel> GetByIdAsync(long id)
        {
            var promoCode = await _repository.GetByIdAsync(id);
            // Checks if there is any promocode with the specified ID    
            if (promoCode == null)
                throw new NotFoundException("The promocode was not found.");

            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<PromoCodeViewModel> GetByNameAsync(string name)
        {
            var promoCode = await _repository.GetByNameAsync(name);
            // Checks if there is any promocode with the specified name    
            if (promoCode == null)
                throw new NotFoundException("The promocode was not found.");

            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<PromoCodeViewModel> AddAsync(AddPromoCodeViewModel model)
        {
            // Checks if there is any promocode with the same name
            if (await _repository.ExistsAsync(model.Name))
                throw new BadRequestException("There is already a promocode with this name.");

            var promoCode = _mapper.Map<AddPromoCodeViewModel, PromoCode>(model);

            await _repository.AddAsync(promoCode);

            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<string> DeleteAsync(long id)
        {
            var promoCode = await _repository.GetByIdAsync(id);
            // Checks if there is any promocode with the specified ID
            if (promoCode == null)
                throw new BadRequestException("The promocode with the specified id does not exist.");

            await _repository.DeleteAsync(promoCode);

            return "The promocode has been deleted successfully";
        }
    }
}
