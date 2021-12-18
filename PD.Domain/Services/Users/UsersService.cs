using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            foreach (string role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            List<User> users = await _repository.GetAllAsync();
            // Checks if there are any users in the database
            if (users.IsNullOrEmpty())
                return new NotFoundObjectResult($"No users were not found");

            return new OkObjectResult(_mapper.Map<List<ShortUserViewModel>>(users));
        }

        public async Task<IActionResult> GetByIdAsync(long id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            // Checks if there is any user with the specified ID    
            if (user == null)
                return new NotFoundObjectResult($"User was not found");

            return new OkObjectResult(_mapper.Map<UserViewModel>(user));
        }

        public async Task<IActionResult> RegisterAsync(RegisterUserModel model)
        {
            // Checks if the given email was already taken
            var emailTaken = await _userManager.FindByEmailAsync(model.Email.Normalize());
            if (emailTaken == null)
                return new BadRequestObjectResult("Email is taken");

            // Checks if the given phone number was already taken
            var phoneTaken = await IsPhoneNumberTakenAsync(model.PhoneNumber);
            if(phoneTaken)
                return new BadRequestObjectResult("Phone number is taken");

            // Trying to add a user to DB
            User user = _mapper.Map<RegisterUserModel, User>(model);
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if(!identityResult.Succeeded)
            {
                return new ObjectResult($"An error occured while trying to add a user to DB, " +
                    $"INFO: {identityResult}");
            }

            await _userManager.AddToRoleAsync(user, RolesNames.USER);

            return new OkObjectResult(_mapper.Map<ShortUserViewModel>(user));
        }

        public async Task<IActionResult> LoginAsync(LoginUserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email.Normalize());
            // Checks if there is a user with the provided email
            if (user == null)
                return new NotFoundObjectResult($"The user with the specified email was not found");

            // Password check
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                return new UnauthorizedObjectResult("Invalid email or password");

            var userRoles = await _userManager.GetRolesAsync(user);
            // Searches for the user's roles (there must be a USER role at least)
            if (userRoles.IsNullOrEmpty())
                return new NotFoundObjectResult("User's roles were not found");

            var authClaims = GetClaims(user, userRoles);
            // Checks if there are any user's claims (there must be two claims at least: userID and USER role)
            if (authClaims.IsNullOrEmpty())
                return new ObjectResult("An error occured while trying to get user's claims");

            var token = GetNewToken(authClaims);
            // Checks whether the token has created successfully 
            if (token == null)
                return new ObjectResult("An error occured while trying to create a token");

            return new OkObjectResult(new
            {
                claims = authClaims,
                roles = userRoles,
                token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                expiration = token.ValidTo,
                id = user.Id
            });
        }

        public async Task<IActionResult> DeleteAsync(long id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            // Checks if there is any user with the specified ID    
            if(user == null)
                return new NotFoundObjectResult("User was not found");

            var result = await _userManager.DeleteAsync(user);
            // Сhecks whether the action was completed successfully
            if (!result.Succeeded)
                return new ObjectResult("An error occured while trying to delete the user");

            return new OkObjectResult(_mapper.Map<UserViewModel>(user));
        }

        public async Task<IActionResult> AddToRole(long userId, string role)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            // Checks if there is any user with the specified ID    
            if (user == null)
                return new NotFoundObjectResult($"User was not found");

            var result = await _userManager.AddToRoleAsync(user, role);
            // Сhecks whether the action was completed successfully
            if (!result.Succeeded)
                return new ObjectResult("Failed to add user to role.");

            return new OkObjectResult(user);
        }

        public async Task<bool> IsPhoneNumberTakenAsync(string phoneNumber)
        {
            return await _repository.IsPhoneTakenAsync(phoneNumber);
        }
    }
}
