using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PD.Domain.Models
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public List<ShortPizzaViewModel> Pizzas { get; set; }
        public string Adress { get; set; }
        public bool IsPaid { get; set; }
        public long UserId { get; set; }
        public long? PromoCodeId { get; set; }
        public float DiscountAmount { get; set; }
        public string DeliveryStatus { get; set; }
        public string DeliveryMethod { get; set; }
        public string Description { get; set; }
    }
}
