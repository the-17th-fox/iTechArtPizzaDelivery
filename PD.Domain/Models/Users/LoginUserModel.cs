using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class LoginUserModel
    {
        [Required (ErrorMessage = "Email is Required")]
        [EmailAddress (ErrorMessage = "Email is Incorrect")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
