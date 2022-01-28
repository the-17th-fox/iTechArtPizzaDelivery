using PD.Domain.Entities;
using PD.Domain.Models;
using PD.Domain.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Interfaces
{
    public interface IUsersRepository
    {
        public PagedList<User> GetAllAsync(PageSettingsViewModel pageSettings);
        public Task<bool> IsPhoneTakenAsync(string phoneNumber);
    }
}
