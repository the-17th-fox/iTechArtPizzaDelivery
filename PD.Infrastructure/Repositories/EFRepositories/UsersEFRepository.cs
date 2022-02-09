﻿using Microsoft.EntityFrameworkCore;
using PD.Domain.Entities;
using PD.Domain.Interfaces;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;
using PD.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Infrastructure.Repositories.EFRepositories
{
    public class UsersEFRepository : IUsersRepository
    {
        private readonly PizzaDeliveryContext _dbContext;
        public UsersEFRepository(PizzaDeliveryContext context) => _dbContext = context;
        
        public PagedList<User> GetAllAsync(PageSettingsViewModel pageSettings)
        {
            IQueryable<User> usersIQuer = _dbContext.Users.AsNoTracking();
            return PagedList<User>.ToPagedList(usersIQuer, pageSettings.PageNumber, pageSettings.PageSize);
        }

        public async Task<bool> IsPhoneTakenAsync(string phoneNumber)
        {
            var isTaken = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();

            return isTaken != null;
        }
    }
}
