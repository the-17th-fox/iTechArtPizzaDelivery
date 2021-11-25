using PD.Domain.Interfaces;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class PromoCodesEFRepository : IPromoCodesRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public PromoCodesEFRepository(PizzaDeliveryContext context) => _dbContext = context;

        public async Task<PromoCode> AddAsync(PromoCode entity)
        {
            var PromoCode = _dbContext.PromoCodes.Add(entity);

            await _dbContext.SaveChangesAsync();
            return PromoCode.Entity;
        }

        public async Task<PromoCode> DeleteAsync(long id)
        {
            PromoCode promoCodeToDelete = await _dbContext.PromoCodes
                .FindAsync(id);

            _dbContext.PromoCodes.Remove(promoCodeToDelete);

            await _dbContext.SaveChangesAsync();

            return promoCodeToDelete;
        }

        public async Task<PromoCode> GetByIdAsync(long id)
        {
            PromoCode promoCode = await _dbContext.PromoCodes.FindAsync(id);
            return promoCode; 
        }

        public async Task<List<PromoCode>> GetAllAsync() => await _dbContext.PromoCodes.ToListAsync();
    }
}
