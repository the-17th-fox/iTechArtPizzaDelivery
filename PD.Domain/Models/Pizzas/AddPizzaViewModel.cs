using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class AddPizzaViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(maximumLength: 20, ErrorMessage = "The length of the string must not be more than 20 characters")]
        public string Name { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "The length of the string must not be more than 150 characters")]
        public string Description { get; set; }
    }
}
