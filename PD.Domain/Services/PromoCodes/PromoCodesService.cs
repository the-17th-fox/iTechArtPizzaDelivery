using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<IActionResult> AddAsync(AddPromoCodeViewModel model)
        {
            // Checks if there is any promocode with the same name
            if (await _repository.ExistsAsync(model.Name))
                return new BadRequestObjectResult("There is already a promo code with this name");

            var promoCode = _mapper.Map<AddPromoCodeViewModel, PromoCode>(model);

            var result = await _repository.AddAsync(promoCode);
            // Checks whether the adding was successful
            if (result == null)
                return new ObjectResult("An error occured while trying to add a new promocode");

            return new OkObjectResult("The promocode has been added successfully");
        }

        public async Task<IActionResult> DeleteAsync(long id)
        {
            // Checks if there is any promocode with the specified ID
            if (await _repository.ExistsAsync(id))
                return new BadRequestObjectResult("The promocode with the specified id does not exist");

            var result = await _repository.DeleteAsync(id);
            // Checks whether the deletion was successful
            if(result == null)
                return new ObjectResult("An error occured while trying to delete the promocode");

            return new OkObjectResult("The promocode has been deleted successfully");
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var promoCodes = await _repository.GetAllAsync();
            // Checks if there is any promocodes 
            if (promoCodes.IsNullOrEmpty())
                return new ObjectResult("No promocodes were found");

            return new OkObjectResult(_mapper.Map<List<ShortPromoCodeViewModel>>(promoCodes));
        }

        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var promoCode = await _repository.GetByIdAsync(id);
            // Checks if there is any promocode with the specified ID    
            if (promoCode == null)
                return new NotFoundObjectResult("Promocode was not found");

            return new OkObjectResult(_mapper.Map<PromoCodeViewModel>(promoCode));
        }
    }
}
