using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class ShortOrderViewModel
    {
        public int Id { get; set; }
        public bool IsPaid { get; set; }
        public int UserId { get; set; }
    }
}
