using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class AddPromoCodeViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(maximumLength: 10, ErrorMessage = "The length of the string must not be more than 10 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Discount Amount is Required")]
        [Range(0, 100.0, ErrorMessage = "The value must be in the range from 0 to 100")]
        public float DiscountAmount { get; set; }

        [StringLength(maximumLength: 150, ErrorMessage = "The length of the string must not be more than 150 characters")]
        public string Description { get; set; }
        
    }
}
