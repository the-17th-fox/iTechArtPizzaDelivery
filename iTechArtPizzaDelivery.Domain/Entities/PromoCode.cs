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
        [Key]
        public int PromoCodeID { get; set; }

        [BindRequired]
        public string Name { get; set; }

        public string Description { get; set; }

        [BindRequired]
        public float DiscountAmount { get; set; }
    }
}
