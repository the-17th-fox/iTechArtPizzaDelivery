using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
