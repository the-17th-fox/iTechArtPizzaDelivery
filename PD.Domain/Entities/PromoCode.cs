using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Entities
{
    public class PromoCode
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte DiscountAmount { get; set; } // 0% - 100%
        public DateTime ExpirationDate { get; set; }
        public List<Order> Orders { get; set; }
    }
}
