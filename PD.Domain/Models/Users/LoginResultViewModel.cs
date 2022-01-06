using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class LoginResultViewModel
    {
        public long Id { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime Lifetime { get; set; }
    }
}
