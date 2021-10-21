using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizza.Web.Entities
{
    public class User
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Pizza> Cart { get; set; }
        public string Addres { get; set; }
        public bool HasAdminRights { get; set; }

        public User(ulong id, string name, string email, string password, List<Pizza> cart = null, string addres = null, bool hasAdminRights = false)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Cart = cart;
            Addres = addres;
            HasAdminRights = hasAdminRights;
        }
    }
}
