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
            var promoCode = await GetAndCheckByIdAsync(id);

            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<PromoCodeViewModel> GetByNameAsync(string name)
        {
            var promoCode = await GetAndCheckByNameAsync(name);

            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<PromoCodeViewModel> AddAsync(AddPromoCodeViewModel model)
        {
            await ExistsAsync(model.Name);

            var promoCode = _mapper.Map<AddPromoCodeViewModel, PromoCode>(model);

            await _repository.AddAsync(promoCode);

            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        public async Task<string> DeleteAsync(long id)
        {
            var promoCode = await GetAndCheckByIdAsync(id);

            await _repository.DeleteAsync(promoCode);

            return "The promocode has been deleted successfully";
        }

        public async Task<PromoCode> GetAndCheckByIdAsync(long id)
        {
            var promoCode = await _repository.GetByIdAsync(id);
            if (promoCode == null)
                throw new NotFoundException("The ingredient with the specified id was not found.");
            return promoCode;
        }

        public async Task<PromoCode> GetAndCheckByNameAsync(string name)
        {
            var promoCode = await _repository.GetByNameAsync(name);
            if (promoCode == null)
                throw new NotFoundException("The ingredient with the specified name was not found.");
            return promoCode;
        }

        public async Task ExistsAsync(string name)
        {
            if(await _repository.ExistsAsync(name))
                throw new BadRequestException("There is already a promocode with this name.");
        }
    }
}
