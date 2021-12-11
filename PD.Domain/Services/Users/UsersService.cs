using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants.AuthOptions;
using PD.Domain.Constants.UsersRoles;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;

namespace PD.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UsersService(IUsersRepository repository, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public JwtSecurityToken GetNewToken(List<Claim> authClaims)
        {
            return new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: authClaims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetKey(), SecurityAlgorithms.HmacSha256)
                );
        }

        public List<Claim> GetClaims(User user, IList<string> userRoles)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, userRoles.ToString())
            };
        }

        public async Task<List<ShortUserViewModel>> GetAllAsync()
        {
            List<User> users = await _repository.GetAllAsync();
            return _mapper.Map<List<ShortUserViewModel>>(users);
        }
        public async Task<UserViewModel> GetByIdAsync(long id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> RegisterAsync(RegisterUserModel model)
        {
            User user = _mapper.Map<RegisterUserModel, User>(model);
            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, RolesNames.USER);
            return _mapper.Map<UserViewModel>(user);
        }

        //public Task<IActionResult> LoginAsync(LoginUserModel model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.Email.Normalize());

        //    if (user == null)
        //        return NotFound($"User with email '{model.Email}' was not found.");

        //    if (!await _userManager.CheckPasswordAsync(user, model.Password))
        //        return Unauthorized("Invalid email or password.");

        //    var userRoles = await _userManager.GetRolesAsync(user);
        //    var authClaims = GetClaims(user, userRoles);
        //    var token = GetNewToken(authClaims);

        //    return Ok(new
        //    {
        //        roles = userRoles,
        //        token = new JwtSecurityTokenHandler()
        //            .WriteToken(token),
        //        expiration = token.ValidTo,
        //        id = user.Id
        //    });
        //}

        public async Task<UserViewModel> DeleteAsync(long id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
