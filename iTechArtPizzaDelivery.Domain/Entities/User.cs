using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class User
    {
        public User(int userID, string name, string phoneNumber, bool hasAdminRights)
        {
            UserID = userID;
            Name = name;
            PhoneNumber = phoneNumber;
            HasAdminRights = hasAdminRights;
        }

        public int UserID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool HasAdminRights { get; set; }

        
    }
}
