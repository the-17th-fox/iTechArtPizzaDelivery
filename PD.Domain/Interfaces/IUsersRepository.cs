using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Interfaces
{
    public interface IUsersRepository
    {
        public Task<List<User>> GetAllAsync();
    }
}
