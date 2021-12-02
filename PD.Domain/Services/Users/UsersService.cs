using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants.AuthOptions;
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

        public JwtSecurityToken GetNewToken(List<Claim> authClaims)
        {
            return new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: authClaims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetKey(), SecurityAlgorithms.DesEncryption)
                );
        }

        public List<Claim> GetUserClaims(User user, IList<string> userRoles)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, userRoles.ToString())
            };
        }
    }
}
