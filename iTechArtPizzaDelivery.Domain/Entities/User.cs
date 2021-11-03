using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        [BindRequired]
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
