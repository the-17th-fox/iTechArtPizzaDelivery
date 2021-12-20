using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class AddIngredientViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(maximumLength: 15, ErrorMessage = "The length of the string must not be more than 15 characters")]
        public string Name { get; set; }
    }
}
