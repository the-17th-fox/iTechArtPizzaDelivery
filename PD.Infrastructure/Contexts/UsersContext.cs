using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Infrastructure.Contexts
{
    public class UsersContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        //public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        //{

        //}

        public UsersContext(DbContextOptions options) : base(options)
        {
        }
    }
}
