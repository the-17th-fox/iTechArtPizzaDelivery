using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IPromoCodesService
    {
        public Task<IActionResult> GetAllAsync();
        public Task<IActionResult> GetByIdAsync(long id);
        public Task<IActionResult> AddAsync(AddPromoCodeViewModel promoCodeModel);
        public Task<IActionResult> DeleteAsync(long id);
    }
}
