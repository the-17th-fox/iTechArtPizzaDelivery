using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Web.Models
{
    public class ShortUserViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public List<IdOnlyOrderViewModel> Order { get; set; }
    }
}
