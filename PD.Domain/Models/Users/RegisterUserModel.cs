using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class RegisterUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Email is Incorrect")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "The length of the string must be in the range from 5 to 30 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(5, ErrorMessage = "The length of the string must be more than 5 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        [Phone(ErrorMessage = "Phone Number is Incorrect")]
        public string PhoneNumber { get; set; }
        
        
    }
}
