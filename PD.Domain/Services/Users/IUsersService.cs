using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllAsync();
        public Task<User> GetByIdAsync(int id);
        //public Task<User> AddUserAsync(string name);
        //public Task<User> DeleteUserAsync(int id);
    }
}
