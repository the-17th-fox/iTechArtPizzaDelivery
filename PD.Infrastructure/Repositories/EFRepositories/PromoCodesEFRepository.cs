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

        public async Task<PromoCode> AddPromoCodeAsync(string name, string description, float discountAmount)
        {
            var PromoCode = _dbContext.PromoCodes
                .Add(new PromoCode
                {
                    Name = name,
                    Description = description,
                    DiscountAmount = discountAmount
                });

            await _dbContext.SaveChangesAsync();
            return PromoCode.Entity;
        }

        public async Task<PromoCode> DeletePromoCodeAsync(int id)
        {
            PromoCode promoCodeToDelete = await _dbContext.PromoCodes
                .FindAsync(id);

            _dbContext.PromoCodes.Remove(promoCodeToDelete);

            await _dbContext.SaveChangesAsync();

            return promoCodeToDelete;
        }

        public async Task<PromoCode> GetPromoCodeAsync(int id) => await _dbContext.PromoCodes.FindAsync(id);

        public async Task<List<PromoCode>> GetPromoCodesAsync() => await _dbContext.PromoCodes.ToListAsync();
    }
}
