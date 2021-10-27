using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class User
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public List<Pizza> Cart { get; set; } // For refactoring
        public string Addres { get; set; }
        public string PhoneNumber { get; set; }
        public bool HasAdminRights { get; set; }

        public User(ulong id, string name, List<Pizza> cart = null, string addres = null, string phoneNumber = null, bool hasAdminRights = false)
        {
            Id = id;
            Name = name;
            Cart = cart;
            Addres = addres;
            PhoneNumber = phoneNumber;
            HasAdminRights = hasAdminRights;
        }
    }
}
