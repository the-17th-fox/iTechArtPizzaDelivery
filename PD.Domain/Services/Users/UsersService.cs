using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants;
using PD.Domain.Constants.AuthOptions;
using PD.Domain.Constants.Exceptions;
using PD.Domain.Constants.UsersRoles;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;

namespace PD.Domain.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UsersService(IUsersRepository usersRepository, IOrdersRepository ordersRepository, IMapper mapper, UserManager<User> userManager)
        {
            _usersRepository = usersRepository;
            _ordersRepository = ordersRepository;
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

        public async Task<PageViewModel<ShortUserViewModel>> GetAllAsync(PageSettingsViewModel pageSettings)
        {
            List<User> users = await _usersRepository.GetAllAsync();

            var pagedList = PagedList<User>.ToPagedList(users, pageSettings.PageNumber, pageSettings.PageSize);

            return _mapper.Map<PagedList<User>, PageViewModel<ShortUserViewModel>>(pagedList);
        }

        public async Task<UserViewModel> GetByIdAsync(long id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            // Checks if there is any user with the specified ID    
            if (user == null)
                throw new NotFoundException("The user was not found");

            var userOrders = await _ordersRepository.GetAllFromUserAsync(user.Id);
            // Checks if there is any orders from user
            if (userOrders == null)
                throw new NotFoundException("The user does not have any ordes.");

            user.Orders = userOrders;

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<ShortUserViewModel> RegisterAsync(RegisterUserModel model)
        {
            var emailTaken = await _userManager.FindByEmailAsync(model.Email.Normalize());
            // Checks if the given email was already taken
            if (emailTaken != null)
                throw new BadRequestException("This email has already been taken.");

            var phoneTaken = await IsPhoneNumberTakenAsync(model.PhoneNumber);
            // Checks if the given phone number was already taken
            if (phoneTaken)
                throw new BadRequestException("This phone number has already been taken.");

            var user = _mapper.Map<RegisterUserModel, User>(model);
            var identityResult = await _userManager.CreateAsync(user, model.Password);
            // Checks whether the adding was successful
            if (!identityResult.Succeeded)
                throw new CreatingFailedException("An error occured during creating the user.");

            var result = await _userManager.AddToRoleAsync(user, RolesNames.USER);
            // Checks whether the adding a user to roles was successful
            if (!result.Succeeded)
                throw new UpdatingFailedException("An error occured during adding the role to the user");

            return _mapper.Map<ShortUserViewModel>(user);
        }

        public async Task<LoginResultViewModel> LoginAsync(LoginUserModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email.Normalize());
            // Checks if there is a user with the provided email
            if (user == null)
                throw new NotFoundException("The user with the specified Email was not found.");

            // Password check
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
                throw new InvalidCredentialsException();

            var userRoles = await _userManager.GetRolesAsync(user);
            // Searches for the user's roles (there must be a USER role at least)
            if (userRoles.IsNullOrEmpty())
                throw new NotFoundException("There are no roles for this user.");

            var authClaims = GetClaims(user, userRoles);
            // Checks if there are any user's claims (there must be two claims at least: userID and USER role)
            if (authClaims.IsNullOrEmpty())
                throw new NotFoundException("There are no claims for this user.");

            var token = GetNewToken(authClaims);
            // Checks whether the token has created successfully 
            if (token == null)
                throw new CreatingFailedException("An error occured during token creation.");

            return new LoginResultViewModel
            {
                Id = user.Id,
                Roles = userRoles,
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token),
                Lifetime = token.ValidTo
            };
        }

        public async Task<string> DeleteAsync(long id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());
            // Checks if there is any user with the specified ID    
            if (user == null)
                throw new NotFoundException("The user with the specified Id was not found.");

            var result = await _userManager.DeleteAsync(user);
            // Сhecks whether the action has completed successfully
            if (!result.Succeeded)
                throw new DeletionFailedException();

            return "The user has been deleted successefully.";
        }

        public async Task<UserRolesViewModel> AddToRole(long userId, string role)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            // Checks if there is any user with the specified ID    
            if (user == null)
                throw new NotFoundException("The user with the specified Id was not found.");

            // Checks whether the user has the specified role
            if (await _userManager.IsInRoleAsync(user, role))
                throw new BadRequestException("The user already has this role.");

            var result = await _userManager.AddToRoleAsync(user, role);
            // Сhecks whether the action has completed successfully
            if (!result.Succeeded)
                throw new UpdatingFailedException("An error occured during adding the user to the role.");

            return new UserRolesViewModel
            {
                Id = userId,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };
        }

        public async Task<bool> IsPhoneNumberTakenAsync(string phoneNumber)
        {
            return await _usersRepository.IsPhoneTakenAsync(phoneNumber);
        }
    }
}
