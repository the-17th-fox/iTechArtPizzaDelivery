using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class OrderIsPaidStatusViewModel
    {
        public long Id { get; set; }
        public bool IsPaid { get; set; }
    }
}
