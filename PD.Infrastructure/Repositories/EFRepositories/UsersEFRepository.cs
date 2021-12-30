using Microsoft.EntityFrameworkCore;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class UsersEFRepository : IUsersRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public UsersEFRepository(PizzaDeliveryContext context) => _dbContext = context;
        
        public async Task<List<User>> GetAllAsync() => await _dbContext.Users.ToListAsync();

        public async Task<bool> IsPhoneTakenAsync(string phoneNumber)
        {
            var isTaken = await _dbContext.Users
                .Where(u => u.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();

            return isTaken != null;
        }
    }
}
