using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{ 
    public class ShortPromoCodeViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public float DiscountAmount { get; set; }
    }
}
