using PD.Domain.Interfaces;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using PD.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class PromoCodesEFRepository : IPromoCodesRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public PromoCodesEFRepository(PizzaDeliveryContext context) => _dbContext = context;

        public async Task<PromoCode> AddAsync(PromoCode promoCode)
        {
            var addedPromoCode = _dbContext.PromoCodes.Add(promoCode);

            await _dbContext.SaveChangesAsync();

            return addedPromoCode.Entity;
        }

        public async Task<PromoCode> DeleteAsync(PromoCode promoCode)
        {
            var deletedPromoCode = _dbContext.PromoCodes.Remove(promoCode);

            await _dbContext.SaveChangesAsync();

            return deletedPromoCode.Entity;
        }

        public async Task<PromoCode> GetByIdAsync(long id) => await _dbContext.PromoCodes.FindAsync(id);

        public async Task<List<PromoCode>> GetAllAsync() => await _dbContext.PromoCodes.ToListAsync();

        public async Task<PromoCode> GetByNameAsync(string name)
        {
            return await _dbContext.PromoCodes
                .Include(p => p.Name == name)
                .FirstAsync();
        }
    }
}
