using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
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
        // TODO: Ask what it is
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IPromoCodesService _promoCodesService;
        public PromoCodesController(IPromoCodesService service) => _promoCodesService = service;

        [ActionName(nameof(GetAllPromoCodesAsync))]
        [HttpGet]
        public async Task<List<PromoCode>> GetAllPromoCodesAsync() => await _promoCodesService.GetAllAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<PromoCode> GetPromoCodeAsync(int id) => await _promoCodesService.GetByIdAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddPromoCodeAsync(string name, string description, float discountAmount)
        {
            PromoCode newPromoCode = await _promoCodesService.AddAsync(name, description, discountAmount);

            return CreatedAtAction(nameof(GetAllPromoCodesAsync), new { id = newPromoCode.PromoCodeID }, newPromoCode);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePromoCodeAsync(int id)
        {
            PromoCode PromoCodeToRemove = await _promoCodesService.DeleteAsync(id);

            return CreatedAtAction(nameof(GetAllPromoCodesAsync), new { id = PromoCodeToRemove.PromoCodeID }, PromoCodeToRemove);
        }
    }
}
