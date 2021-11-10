using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD.Domain.Entities;
using PD.Domain.Interfaces;

namespace PD.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        public UsersService(IUsersRepository repository) => _repository = repository;

        public async Task<User> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task<List<User>> GetAllAsync() => await _repository.GetAllAsync();
    }
}
