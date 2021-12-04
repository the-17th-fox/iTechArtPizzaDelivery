using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PD.Domain.Constants.AuthOptions;
using PD.Domain.Entities;
using PD.Domain.Interfaces;

namespace PD.Domain.Services
{
    public class UsersService : IUsersService
    {
        public JwtSecurityToken GetNewToken(List<Claim> authClaims)
        {
            return new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: authClaims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetKey(), SecurityAlgorithms.HmacSha256)
                );
        }

        public List<Claim> GetUserClaims(User user, IList<string> userRoles)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, userRoles.ToString())
            };
        }
    }
}
