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
        public PageViewModel<ShortUserViewModel> GetAllAsync(PageSettingsViewModel pageSettings);
        public Task<UserViewModel> GetByIdAsync(long id);
        public Task<ShortUserViewModel> RegisterAsync(RegisterUserModel model);
        public Task<LoginResultViewModel> LoginAsync(LoginUserModel model);
        public Task<string> DeleteAsync(long id);
        public Task<UserRolesViewModel> AddToRole(long userId, string role);
        public Task<bool> IsPhoneNumberTakenAsync(string phoneNumber);
        public Task<UserNamesViewModel> ChangeNamesAsync(long userId, ChangeNamesViewModel model);
        public Task<string> ChangePasswordAsync(long userId, ChangePasswordViewModel model);
    }
}
