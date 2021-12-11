using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class AddOrderViewModel
    {
        [Required]
        public long UserId { get; set; }
    }
}
