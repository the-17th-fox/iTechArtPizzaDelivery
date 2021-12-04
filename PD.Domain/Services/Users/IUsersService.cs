using PD.Domain.Entities;
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
        public List<Claim> GetUserClaims(User user, IList<string> userRoles);
    }
}
