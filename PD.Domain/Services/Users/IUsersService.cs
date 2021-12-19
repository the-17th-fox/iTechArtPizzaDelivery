using Microsoft.AspNetCore.Mvc;
using PD.Domain.Entities;
using PD.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PD.Domain.Services
{
    public interface IUsersService
    {
        public JwtSecurityToken GetNewToken(List<Claim> authClaims);
        public List<Claim> GetClaims(User user, IList<string> userRoles);
        public Task<IActionResult> GetAllAsync();
        public Task<IActionResult> GetByIdAsync(long id);
        public Task<IActionResult> RegisterAsync(RegisterUserModel model);
        public Task<IActionResult> LoginAsync(LoginUserModel model);
        public Task<IActionResult> DeleteAsync(long id);
        public Task<IActionResult> AddToRole(long userId, string role);
        public Task<bool> IsPhoneNumberTakenAsync(string phoneNumber);
    }
}
