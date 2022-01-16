using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class PageSettingsViewModel
    {
        [Required(ErrorMessage = "Page size is required.")]
        [Range(minimum: 1, maximum: 50, ErrorMessage = "Invalid page size.")]
        public int PageSize { get; set; }

        [Required(ErrorMessage = "Page number is required.")]
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Invalid page number.")]
        public int PageNumber { get; set; } = 1;
    }
}
