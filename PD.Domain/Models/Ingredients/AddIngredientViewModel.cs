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
        [Required]
        public string Name { get; set; }
    }
}
