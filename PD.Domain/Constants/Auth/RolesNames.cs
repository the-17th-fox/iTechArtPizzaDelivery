using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Constants.UsersRoles
{
    public class RolesNames
    {
        public const string USER = "User";
        public const string ADMIN = "Administrator";

        private static readonly List<IdentityRole<long>> Roles = new List<IdentityRole<long>>()
        {
            new IdentityRole<long>(USER) { Id = 1, NormalizedName = "USER"},
            new IdentityRole<long>(ADMIN) { Id = 2, NormalizedName = "ADMINISTRATOR" }
        };

        public static List<IdentityRole<long>> GetRolesNames()
        {
            return Roles;
        }
    }
}
