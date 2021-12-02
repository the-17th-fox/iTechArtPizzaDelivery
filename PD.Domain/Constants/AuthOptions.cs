using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Constants.AuthOptions
{
    public class AuthOptions
    {
        public const string ISSUER = "PizzaDeliveryServer";
        public const string AUDIENCE = "PizzaDeliveryClient";
        public const int LIFETIME = 1;
        private const string KEY = "asdfghjklfoxlkjhgfdsa";
        public static SymmetricSecurityKey GetKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
