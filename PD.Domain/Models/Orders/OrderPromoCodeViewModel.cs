using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class OrderPromoCodeViewModel
    {
        public long Id { get; set; }
        public long? PromoCodeId { get; set; }
        public float DiscountAmount { get; set; }
    }
}
