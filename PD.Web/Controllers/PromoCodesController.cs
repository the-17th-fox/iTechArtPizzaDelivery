using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    public class PromoCodesController : Controller
    {
        // TODO: Ask what it is
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private readonly IPromoCodesService _PromoCodesService;
        public PromoCodesController(IPromoCodesService PromoCodesService) => _PromoCodesService = PromoCodesService;

        [ActionName(nameof(GetAllPromoCodesAsync))]
        [HttpGet]
        public async Task<List<PromoCode>> GetAllPromoCodesAsync() => await _PromoCodesService.GetPromoCodesAsync();

        [Route("{id}")]
        [HttpGet]
        public async Task<PromoCode> GetPromoCodeAsync(int id) => await _PromoCodesService.GetPromoCodeAsync(id);

        [HttpPost]
        public async Task<ActionResult> AddPromoCodeAsync(string name, string description, float discountAmount)
        {
            PromoCode newPromoCode = await _PromoCodesService.AddPromoCodeAsync(name, description, discountAmount);

            return CreatedAtAction(nameof(GetAllPromoCodesAsync), new { id = newPromoCode.PromoCodeID }, newPromoCode);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePromoCodeAsync(int id)
        {
            PromoCode PromoCodeToRemove = await _PromoCodesService.DeletePromoCodeAsync(id);

            return CreatedAtAction(nameof(GetAllPromoCodesAsync), new { id = PromoCodeToRemove.PromoCodeID }, PromoCodeToRemove);
        }
    }
}
