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

        public async Task<User> GetByIdAsync(long id)
        {
            return await _dbContext.Users
                .Include(u => u.Order)
                .Where(u => u.Id == id)
                .FirstAsync();
        }

        public async Task<List<User>> GetAllAsync() => await _dbContext.Users.ToListAsync();

        public async Task<User> AddAsync(User entity)
        {
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<User> DeleteAsync(long id)
        {
            User userToDelete = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
            return userToDelete;
        }
    }
}
