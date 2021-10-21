using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizza.Web.Entities
{
    public class PromoCode
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float DiscountAmount { get; set; }

        public PromoCode(ulong id, string title, string description, float discountAmount)
        {
            Id = id;
            Name = title;
            Description = description;
            DiscountAmount = discountAmount;
        }
    }
}
