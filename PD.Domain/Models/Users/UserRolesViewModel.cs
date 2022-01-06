using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class UserRolesViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
