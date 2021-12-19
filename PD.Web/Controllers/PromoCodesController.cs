using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Services;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodesController : Controller
    {
        private readonly IPromoCodesService _promoCodesService;
        public PromoCodesController(IPromoCodesService service)
        {
            _promoCodesService = service;
        }

        [Route("all")]
        [ActionName(nameof(GetAllAsync))]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _promoCodesService.GetAllAsync());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            return Ok(await _promoCodesService.GetByIdAsync(id));
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddPromoCodeViewModel promoCodeModel)
        {
            if (!ModelState.IsValid)
                return new BadRequestObjectResult("Model is invalid");

            return Ok(await _promoCodesService.AddAsync(promoCodeModel));
        }

        [Route("[action]/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _promoCodesService.DeleteAsync(id));
        }
    }
}
