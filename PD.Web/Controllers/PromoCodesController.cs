using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodesController : Controller
    {
        private readonly IPromoCodesService _promoCodesService;
        private readonly IMapper _mapper;
        public PromoCodesController(IPromoCodesService service, IMapper mapper)
        {
            _promoCodesService = service;
            _mapper = mapper;
        }

        [Route("all")]
        [ActionName(nameof(GetAllAsync))]
        [HttpGet]
        public async Task<List<ShortPromoCodeViewModel>> GetAllAsync()
        {
            List<PromoCode> promoCodes = await _promoCodesService.GetAllAsync();
            return _mapper.Map<List<ShortPromoCodeViewModel>>(promoCodes);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<PromoCodeViewModel> GetPromoCodeAsync(int id)
        {
            PromoCode promoCode = await _promoCodesService.GetByIdAsync(id);
            return _mapper.Map<PromoCodeViewModel>(promoCode);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ShortPromoCodeViewModel> AddAsync(AddPromoCodeModel promoCodeModel)
        {
            PromoCode newPromoCode = _mapper.Map<AddPromoCodeModel, PromoCode>(promoCodeModel);
            await _promoCodesService.AddAsync(newPromoCode);
            return _mapper.Map<ShortPromoCodeViewModel>(newPromoCode);
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<ShortPromoCodeViewModel> DeleteAsync(int id)
        {
            PromoCode promoCodeToRemove = await _promoCodesService.DeleteAsync(id);
            return _mapper.Map<ShortPromoCodeViewModel>(promoCodeToRemove);
        }
    }
}
