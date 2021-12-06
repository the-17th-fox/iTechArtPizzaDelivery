using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class ShortOrderViewModel
    {
        public long Id { get; set; }
        public bool IsPaid { get; set; }
        public long UserId { get; set; }
    }
}
