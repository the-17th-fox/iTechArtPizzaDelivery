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

        public async Task<User> GetByIdAsync(long id) => await _repository.GetByIdAsync(id);

        public async Task<List<User>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<User> AddAsync(User entity) => await _repository.AddAsync(entity);

        public async Task<User> DeleteAsync(long id) => await _repository.DeleteAsync(id);
    }
}
