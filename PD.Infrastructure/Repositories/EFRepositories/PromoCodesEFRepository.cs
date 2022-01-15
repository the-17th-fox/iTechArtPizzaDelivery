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
using PD.Domain.Constants.Exceptions;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class PromoCodesEFRepository : IPromoCodesRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public PromoCodesEFRepository(PizzaDeliveryContext context) => _dbContext = context;

        public async Task AddAsync(PromoCode promoCode)
        {

            try
            {
                _dbContext.PromoCodes.Add(promoCode);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new CreatingFailedException();
            }
        }

        public async Task DeleteAsync(PromoCode promoCode)
        {
            try
            {
                _dbContext.PromoCodes.Remove(promoCode);

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new DeletionFailedException();
            }
        }

        public async Task<PromoCode> GetByIdAsync(long id) => await _dbContext.PromoCodes.FindAsync(id);

        public async Task<List<PromoCode>> GetAllAsync() => await _dbContext.PromoCodes.ToListAsync();

        public async Task<PromoCode> GetByNameAsync(string name)
        {
            return await _dbContext.PromoCodes
                .Where(pr => pr.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(long id)
        {
            var promoCode = await _dbContext.PromoCodes
                .FindAsync(id);

            return promoCode != null;
        }

        public async Task<bool> ExistsAsync(string name)
        {
            var promoCode = await _dbContext.PromoCodes
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();

            return promoCode != null;
        }
    }
}
