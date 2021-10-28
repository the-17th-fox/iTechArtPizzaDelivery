using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTechArtPizzaDelivery.Domain.Entities
{
    public class PromoCode
    {
        public PromoCode(int promoCodeID, string name, string description, float discountAmount)
        {
            PromoCodeID = promoCodeID;
            Name = name;
            Description = description;
            DiscountAmount = discountAmount;
        }

        public int PromoCodeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float DiscountAmount { get; set; }

        
    }
}
