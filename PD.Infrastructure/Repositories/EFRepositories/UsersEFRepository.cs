using Microsoft.EntityFrameworkCore;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class UsersEFRepository : IUsersRepository
    {
        private readonly UsersContext _dbContext;
        public UsersEFRepository(UsersContext context) => _dbContext = context;

        public async Task<User> GetByIdAsync(int id) => await _dbContext.Users.FindAsync(id);

        public async Task<List<User>> GetAllAsync() => await _dbContext.Users.ToListAsync();
    }
}
